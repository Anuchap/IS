using System.Data.Entity;
using Data;
using Domain.Entities;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Type = Domain.Entities.Type;

namespace WinService
{
    internal class Program
    {
        private static void Run()
        {
            Log("Scaning...");

            List<Site> sites;

            using (var db = new Context())
            {
                sites = db.Sites.ToList();
            }

            var now = DateTime.Now;

            foreach (var site in sites)
            {
                var site1 = site;
                switch (site1.Type)
                {
                    case Type.Ping:
                        new Thread(() =>
                        {
                            using (var db = new Context())
                            {
                                var s = db.Sites.Include(p => p.PatternTimes).Include(d => d.Downtimes).Single(x => x.Id == site1.Id);

                                var office = s.PatternTimes.Any( x => x.Days.Contains(now.DayOfWeek.ToString()) && x.TimeOpen <= now.Hour && now.Hour < x.TimeClose) ? Office.Open : Office.Closed;

                                var tr = new Transaction { Office = office, ResponseTime = DateTime.Now, Type = Type.Ping };

                                try
                                {
                                    var reply = new Ping().Send(IPAddress.Parse(site1.Ip), 10 * 1000); // 1 minute time out (in ms)

                                    tr.Status = (reply != null && reply.Status == IPStatus.Success) ? Status.Up : Status.Dn;
                                }
                                catch (Exception)
                                {
                                    tr.Status = Status.Dn;
                                }

                                UpdateStatus(s, tr);
                                db.SaveChanges();
                            }
                        }).Start();
                        break;

                    case Type.Snmp:
                        new Thread(() =>
                        {
                            using (var db = new Context())
                            {
                                var s = db.Sites.Include(p => p.PatternTimes).Include(d => d.Downtimes).Single(x => x.Id == site1.Id);

                                var office = s.PatternTimes.Any( x => x.Days.Contains(now.DayOfWeek.ToString()) && x.TimeOpen <= now.Hour && now.Hour < x.TimeClose) ? Office.Open : Office.Closed;

                                var tr = new Transaction { Office = office, ResponseTime = DateTime.Now, Type = Type.Snmp };

                                try
                                {
                                    Messenger.Get(VersionCode.V1, new IPEndPoint(IPAddress.Parse(site1.Ip), 161), new OctetString(site1.Community), new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")) }, 10 * 1000); // 1 minute time out (in ms)

                                    tr.Status = Status.Up;
                                }
                                catch (Exception)
                                {
                                    tr.Status = Status.Dn;
                                }

                                UpdateStatus(s, tr);
                                db.SaveChanges();
                            }
                        }).Start();
                        break;
                }
            }
        }

        private static void UpdateStatus(Site site, Transaction transaction)
        {
            site.Transactions.Add(transaction);
            site.Status = transaction.Status;
            site.Office = transaction.Office;
            site.LastUpdate = transaction.ResponseTime;

            var dt = site.Downtimes.SingleOrDefault(d => d.UpTime == null);

            if (dt == null)
            {
                if (transaction.Status == Status.Dn)
                {
                    site.Downtimes.Add(new Downtime
                    {
                        DownTime = transaction.ResponseTime,
                        Office = transaction.Office
                    });
                }
            }
            else if (transaction.Status == Status.Up)
            {
                dt.UpTime = transaction.ResponseTime;
                dt.DurationTime = (transaction.ResponseTime - dt.DownTime).Minutes;
            }

            Log(site.Ip + " [" + transaction.Type + ":" + transaction.Status + "]");
        }

        private static void Log(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg);
        }

        private static void Main(string[] args)
        {
            try
            {
                Log("Service Started.");
                Run();
                var interval = Convert.ToInt16(ConfigurationManager.AppSettings["IntervalTime"]) * 60 * 1000;
                var timer = new System.Timers.Timer(interval);
                timer.Elapsed += (sender, eventArgs) => Run();
                timer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Read();
        }
    }
}