using System;

namespace Domain.Entities
{
    public class Transaction : EntityBase
    {
        public DateTime ResponseTime { get; set; }

        public Status Status { get; set; }

        public Office Office { get; set; }

        public Type Type { get; set; }

        public Site Site { get; set; }
    }
}