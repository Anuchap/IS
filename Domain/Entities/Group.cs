using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities
{
    public class Group : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Site> Sites { get; set; }

        public Group()
        {
            Sites = new List<Site>();
        }
    }
}