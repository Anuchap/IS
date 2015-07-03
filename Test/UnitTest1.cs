using System;
using System.Data.Entity;
using System.Linq;
using Data;
using Data.Repositories;
using Domain.BusinessObjects;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Type = Domain.Entities.Type;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddTransaction()
        {
            using (var db = new Context())
            {
                var site = new Site
                {
                    Code = "KKC03",
                    Name = "คังเซนฯ สุพรรณบุรี",
                    Ip = "192.168.13.254",
                    Type = Type.Snmp,
                    Isp = Isp.Tot,
                    Phone = "035-451629"
                };

                var trans = new Transaction
                {
                    Status = Status.Up,
                    //ResponseTime = DateTime.Now,
                    Type = Type.Snmp
                };

                site.Transactions.Add(trans);

                db.Sites.Add(site);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void TestGetLastTransaction()
        {
            using (var db = new Context())
            {
                var result = from s in db.Sites.ToList()
                             select new { a = s, b = s.Transactions.OrderByDescending(x => x.ResponseTime).First() };

                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
        }

        [TestMethod]
        public void SetSitesType()
        {
            using (var db = new Context())
            {
                foreach (var site in db.Sites)
                {
                    site.Type = Type.Snmp;
                }
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void CheckOfficeOpened()
        {
            using (var db = new Context())
            {
                var s = db.Sites.Single(x => x.Id == 124);

                var now = DateTime.Now; //Parse("2015-02-23 12:00:00");

                var result = s.PatternTimes.Any(x => x.Days.Contains(now.DayOfWeek.ToString()) && x.TimeOpen <= now.Hour && now.Hour < x.TimeClose);
            }
        }

        [TestMethod]
        public void GetSites()
        {
            var uow = new UnitOfWork(new Context());

            var allSites = uow.MonitorRepo.GetSitesByGroup();

            var top10 = uow.MonitorRepo.GetSitesFailed();
        }

        [TestMethod]
        public void TestEagerLoading()
        {
            using (var db = new Context())
            {
                var sites = db.Sites.Include(x => x.PatternTimes).ToList();
            }
        }

        [TestMethod]
        public void GetTopSiteFailed()
        {
            using (var db = new Context())
            {
                var q = from t in db.Transactions.Include(s => s.Site)
                        where t.Status == Status.Dn && t.Office == Office.Open
                        group t by t.Site
                            into g
                            orderby g.Count() descending
                            select new SiteFailed { Site = g.Key, DownCount = g.Count() };
            }
        }

        [TestMethod]
        public void TestDowntime()
        {
            using (var db = new Context())
            {
                var tr = new Transaction { Office = Office.Open, ResponseTime = DateTime.Now, Type = Type.Ping, Status = Status.Dn };

                var s = db.Sites.Include(d => d.Downtimes).Single(x => x.Id == 164);

                var dt = s.Downtimes.SingleOrDefault(d => d.UpTime == null);

                if (dt == null)
                {
                    if (tr.Status == Status.Dn)
                    {
                        s.Downtimes.Add(new Downtime
                        {
                            DownTime = tr.ResponseTime
                        });
                    }
                }
                else if (tr.Status == Status.Up)
                {
                    dt.UpTime = tr.ResponseTime;
                    dt.DurationTime = (tr.ResponseTime - dt.DownTime).Minutes;
                }

                db.SaveChanges();
            }
        }

        [TestMethod]
        public void GetDowntimesBySiteId()
        {
            var m = new MonitorRepo(new Context());
            var dt = m.GetDowntimesBySiteId(124);

        }

        [TestMethod]
        public void GetEmployees()
        {
            using (var db = new Context())
            {
                //var result = db.Employees.ToList();
            }
        }
    }
}
