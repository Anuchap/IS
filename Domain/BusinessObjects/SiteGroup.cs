using System.Collections.Generic;
using Domain.Entities;

namespace Domain.BusinessObjects
{
    public class SiteGroup
    {
        public string Group { get; set; }

        public List<Site> Sites { get; set; }
    }
}