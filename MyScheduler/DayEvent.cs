using System;
using System.Collections.Generic;
using System.Text;

namespace MyScheduler
{
    public class DayEvent
    {

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

        public string eventName { get; private set; }

        public string Description { get; set; }

        public DayEvent()
        {
            eventName = "some event";
            StartEventDate = DateTime.Now;
            EndEventDate = DateTime.Now.AddDays(1);
        }

        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate) : this()
        {
            this.eventName = eventName;
            StartEventDate = startEventDate;
            EndEventDate = endEventDate;
            Description = "";
        }

        public DayEvent(string eventName, DateTime startEventDate, DateTime endEventDate, string description) : this(eventName, startEventDate, endEventDate)
        {
            Description = description;
        }

        public void PrintEvent()
        {
            Console.Write("Event name : ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{ eventName}");
            Console.ResetColor();
            Console.WriteLine($"Start event : {startEventDate:f}\nEnd event : {endEventDate:f}\n" +
                $"Event duration : {getEventDuration(startEventDate, endEventDate).ToString(@"dd\.hh\:mm")}");
            if(Description.Length != 0) { Console.WriteLine($"Description : {Description}"); }

        }

        public string getInfo()
        {
            string finalString = 
                $"Event name : {eventName}\nStart event : {startEventDate:f}\nEnd event : {endEventDate:f}\n" +
                $"Event duration : {getEventDuration(startEventDate, endEventDate).ToString(@"dd\.hh\:mm")}\n";

            if (Description != "") { finalString += $"Description : {Description}"; }
            return finalString;
        }

        public TimeSpan getEventDuration(DateTime start, DateTime end)
        {
            return end - start;
        }

    } 
}
