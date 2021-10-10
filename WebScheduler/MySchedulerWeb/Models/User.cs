using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Role id")]
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public virtual List<DayEvent> DayEvents { get; set; }

    }
}
