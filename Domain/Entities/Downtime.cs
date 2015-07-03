using System;
using Newtonsoft.Json;

namespace Domain.Entities
{
    public class Downtime : EntityBase
    {
        public DateTime DownTime { get; set; }

        public DateTime? UpTime { get; set; }

        public int DurationTime { get; set; }

        public Office Office { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }
    }
}