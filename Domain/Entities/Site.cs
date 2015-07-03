using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Site : EntityBase
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Ip { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }

        public string Community { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Isp Isp { get; set; }

        public string Phone { get; set; }

        public Group Group { get; set; }

        [JsonIgnore]
        public ICollection<PatternTime> PatternTimes { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Office Office { get; set; }

        public DateTime LastUpdate { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }

        [JsonIgnore]
        public ICollection<Downtime> Downtimes { get; set; }

        [JsonIgnore]
        public int DowntimeLimit { get; set; }

        public Site()
        {
            Transactions = new List<Transaction>();
            Downtimes = new List<Downtime>();
        }
    }
}