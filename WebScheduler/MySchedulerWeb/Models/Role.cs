using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class Role
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
