using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class DayEvent
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string Description { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
