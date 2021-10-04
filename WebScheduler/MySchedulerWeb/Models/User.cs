using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; private set; }

        public virtual List<DayEvent> DayEvents { get; set; }

    }
}
