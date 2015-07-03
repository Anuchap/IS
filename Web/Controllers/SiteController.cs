using Data;
using Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly Context _db = new Context();

        // GET: Site
        public ActionResult Index()
        {
            return View(_db.Sites.Include(g => g.Group).ToList());
        }

        // GET: Site/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _db.Sites.Include(g => g.Group).Single(s => s.Id == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: Site/Create
        public ActionResult Create()
        {
            ViewBag.Groups = _db.Groups.ToList();
            return View();
        }

        // POST: Site/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Ip,Type,Isp,Phone,Status,Office,LastUpdate,DowntimeLimit,Community")] Site site, int groupId)
        {
            if (ModelState.IsValid)
            {
                site.Group = _db.Groups.Find(groupId);
                site.LastUpdate = DateTime.Now;
                _db.Sites.Add(site);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(site);
        }

        // GET: Site/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _db.Sites.Include(g => g.Group).Single(x => x.Id == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.Groups = _db.Groups.ToList();
            return View(site);
        }

        // POST: Site/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Ip,Type,Isp,Phone,Status,Office,LastUpdate,DowntimeLimit,Community")] Site site, int groupId)
        {
            if (ModelState.IsValid)
            {
                var s = _db.Sites.Single(x => x.Id == site.Id);
                s.Code = site.Code;
                s.Name = site.Name;
                s.Ip = site.Ip;
                s.Type = site.Type;
                s.Isp = site.Isp;
                s.Phone = site.Phone;
                s.Group = _db.Groups.Find(groupId);
                s.DowntimeLimit = site.DowntimeLimit;
                s.Community = site.Community;
                //_db.Entry(site).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: Site/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _db.Sites.Include(g => g.Group).Single(x => x.Id == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Site/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = _db.Sites.Find(id);

            _db.Downtimes.RemoveRange(_db.Downtimes.Include(x => x.Site).Where(x => x.Site.Id == site.Id));
            _db.Transactions.RemoveRange(_db.Transactions.Include(x => x.Site).Where(x => x.Site.Id == site.Id));

            _db.Sites.Remove(site);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ResetDowntimeLimit(int downtimeLimit)
        {
            _db.Sites.ForEach(s => s.DowntimeLimit = downtimeLimit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}