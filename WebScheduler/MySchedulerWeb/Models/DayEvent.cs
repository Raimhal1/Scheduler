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

        [Required(ErrorMessage = "Event name not specified")]
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Start Date not specified")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start date")]
        public DateTime StartEventDate { get; set; }

        [Required(ErrorMessage = "End Date not specified")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        public DateTime EndEventDate { get; set; }

        public string Description { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
