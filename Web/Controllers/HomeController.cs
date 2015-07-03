using System.Web.Mvc;
using System.Web.Security;
using Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _uow;

        public HomeController()
        {
            _uow = new UnitOfWork(new Context());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMonitor()
        {
            var monitor = new MonitorModel
            {
                SiteGroups = _uow.MonitorRepo.GetSitesByGroup(),
                SiteFaileds = _uow.MonitorRepo.GetSitesFailed()
            };

            return new JsonNetResult(monitor);
        }

        public ActionResult GetDowntimesBySiteId(int siteId)
        {
            return new JsonNetResult(_uow.MonitorRepo.GetDowntimesBySiteId(siteId));
        }
    }
}