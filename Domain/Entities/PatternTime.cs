using System.Collections.Generic;

namespace Domain.Entities
{
    public class PatternTime : EntityBase
    {
        public int Key { get; set; }

        public string Days { get; set; }

        public int TimeOpen { get; set; }

        public int TimeClose { get; set; }

        public ICollection<Site> Sites { get; set; }
    }
}