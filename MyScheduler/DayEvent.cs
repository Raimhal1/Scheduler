using System;
using System.Collections.Generic;
using System.Text;

namespace MyScheduler
{
    public class DayEvent
    {

        private string eventName;
        private DateTime startEventDate, endEventDate;
        public DateTime StartEventDate {
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
            eventName = "some event";
            StartEventDate = DateTime.Now;
            EndEventDate = DateTime.Now.AddDays(1);
            Description = "";
        }

        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate) : base()
        {
            this.eventName = eventName;
            StartEventDate = startEventDate;
            EndEventDate = endEventDate;

        }
        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate, string descriptioin) : this(eventName, startEventDate, endEventDate)
        {
            Description = descriptioin;
        }

        public void PrintEvent()
        {
            Console.WriteLine($"Event name : {eventName}\nStart event : {startEventDate:f}\n" +
                $"End event : {endEventDate:f}\nEvent duration : {getEventDuration(startEventDate, endEventDate).ToString(@"dd\.hh\:mm")}\n" +
                $"Description :\n {Description}");
        }

        public TimeSpan getEventDuration(DateTime start, DateTime end)
        {
            return end - start;
        }

    } 
}
