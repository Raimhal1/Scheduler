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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm}")]
        public DateTime StartEventDate { get; set; }

        [Required(ErrorMessage = "End Date not specified")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm}")]
        public DateTime EndEventDate { get; set; }

        [Display(Name = "Short description")]
        [StringLength(30)]
        public string ShortDescription { get; set; }

        [Display(Name = "Long description")]
        public string Description { get; set; }

        public string Creator { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
