using Domain.Entities;

namespace Domain.BusinessObjects
{
    public class SiteFailed
    {
        public Site Site { get; set; }

        public int DownCount { get; set; }
    }
}