using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class DayEvent
    {
        public List<User> members = new List<User>();
        public int Id { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.DateTime)]
        private DateTime startEventDate, endEventDate;
        public DateTime StartEventDate
        {
            get
            {
                return startEventDate;
            }
            set
            {
                startEventDate = value;
            }
        }
        public DateTime EndEventDate
        {
            get
            {
                return endEventDate;
            }
            set
            {
                endEventDate = value;
            }
        }
        public string Description { get; set; }

        public DayEvent()
        {
            Id = 0;
            EventName = "some event";
            StartEventDate = DateTime.Now;
            EndEventDate = DateTime.Now.AddDays(1);
        }

        public DayEvent(int id, string eventName, DateTime startEventDate, DateTime endEventDate) : this()
        {
            this.Id = id;
            this.EventName = eventName;
            StartEventDate = startEventDate;
            EndEventDate = endEventDate;
            Description = "";
        }

        public DayEvent(int id, string eventName, DateTime startEventDate, DateTime endEventDate, string description) : this(id, eventName, startEventDate, endEventDate)
        {
            Description = description;
        }

        public (string, DateTime, DateTime, TimeSpan, string) getInfo()
        {
            return (
                EventName, startEventDate, endEventDate,
                getEventDuration(startEventDate, endEventDate),
                Description
                );

        }

        private TimeSpan getEventDuration(DateTime start, DateTime end)
        {
            return end - start;
        }

    }
}
