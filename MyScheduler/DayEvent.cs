using System;
using System.Collections.Generic;

namespace MyScheduler
{
    public class DayEvent
    {

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

        public List<User> members;
        public string EventName { get; private set; }
        public string Description { get; set; }


        public DayEvent()
        {
            EventName = "some event";
            StartEventDate = DateTime.Now;
            EndEventDate = DateTime.Now.AddDays(1);
        }

        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate) : this()
        {
            this.EventName = eventName;
            StartEventDate = startEventDate;
            EndEventDate = endEventDate;
            Description = "";
        }

        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate, string description) : this(eventName, startEventDate, endEventDate)
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
