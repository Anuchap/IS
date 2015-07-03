using System.Collections.Generic;
using Domain.BusinessObjects;

namespace Web.Models
{
    public class MonitorModel
    {
        public List<SiteGroup> SiteGroups { get; set; }

        public List<SiteFailed> SiteFaileds { get; set; }
    }
}