using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public List<Module> Modules { get; set; } 

        public Role()
        {
            Users = new List<User>();
        }
    }
}