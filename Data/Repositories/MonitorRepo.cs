using Domain.BusinessObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repositories
{
    public class MonitorRepo
    {
        private readonly Context _context;

        public MonitorRepo(Context context)
        {
            _context = context;
        }

        public List<SiteGroup> GetSitesByGroup()
        {
            var query = from s in _context.Sites
                        group s by s.Group.Name
                            into g
                            select new SiteGroup { Group = g.Key, Sites = g.ToList() };

            return query.ToList();
        }

        public List<SiteFailed> GetSitesFailed()
        {
            var today = DateTime.Today;
            var query = from d in _context.Downtimes.Include(s => s.Site)
                        where DbFunctions.TruncateTime(d.DownTime) == today && d.Office == Office.Open
                        group d by d.Site
                            into g
                            orderby g.Count() descending
                            select new SiteFailed { Site = g.Key, DownCount = g.Count() };

            return query.Take(10).ToList();
        }

        public List<Downtime> GetDowntimesBySiteId(int siteId)
        {
            var today = DateTime.Today;
            var query = from d in _context.Downtimes.Include(s => s.Site)
                where d.Site.Id == siteId && DbFunctions.TruncateTime(d.DownTime) == today
                select d;

            return query.ToList();
        }
    }
}