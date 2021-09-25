using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
/// <summary>
/// ---menu---
/// remote event
/// add description
/// ---functionality---
/// delete from list
/// modified description
/// ---print---
/// print all event per day, week, month
/// </summary>
namespace MyScheduler
{

    class Program
    {
        public static List<DayEvent> events;

        static void Main(string[] args)
        {
            events = new List<DayEvent>();
            CreateNewEvent();
            PrintEventList(events);
        }

        static void CreateDate(out DateTime date, string state = "start")
        {
            try
            {
                Console.Write($"Enter event {state} date : ");
                var str = Console.ReadLine();

                Regex reg = new Regex(@"[/]");
                str = reg.Replace(str, ".");

                var check = DateTime.ParseExact(str, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                date = check;
            }
            catch (FormatException)
            {
                Console.WriteLine("Date is not in the proper format. Try again!");
                CreateDate(out date);
                return;
            }
        }

        static void CreateTime(in DateTime date, out DateTime date_and_time, string state = "start")
        {
            try
            {
                Console.Write($"Enter event {state} time : ");
                var str = Console.ReadLine();

                var reg = new Regex(@"(\d+):(\d{2})");
                Match match = reg.Match(str);

                if (!match.Success){throw new FormatException();}

                int hours = int.Parse(match.Groups[1].Value), minutes = int.Parse(match.Groups[2].Value);
                date_and_time = new DateTime(date.Year, date.Month, date.Day, hours, minutes, 0);
            }
            catch (FormatException)
            {
                Console.WriteLine("Time is not in the proper format. Try again!");
                CreateTime(in date, out date_and_time, state);
                return;
            }
        }

        static void CreateNewEvent()
        {
            // set event name
            string eventName = "";
            try
            {
                Console.Write("Enter event name : ");
                eventName = Console.ReadLine();
                if(eventName.Length == 0){throw new InvalidOperationException();}
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("The event needs a name! Try again!");
                CreateNewEvent();
                return;
            }

            // set event start time
            DateTime eventDate;
            CreateDate(out eventDate);
            DateTime eventStartDate;
            CreateTime(in eventDate, out eventStartDate);

            // set event end time
            CreateDate(out eventDate, "end");
            DateTime eventEndDate;
            CreateTime(in eventDate, out eventEndDate, "end");

            try
            {
                if(eventEndDate < eventStartDate) { throw new InvalidOperationException(); }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Such an event cannot exist!\n" +
                    "The end date of the event must be later than the start date of the event!");
                Console.WriteLine("Try again to create an event.");
                CreateNewEvent();
                return;
            }

            // set event description
            Console.Write("Enter event description : ");
            string description = Console.ReadLine();

            // create new event
            DayEvent newEvent = new DayEvent(eventName, eventStartDate, eventEndDate, description);
            events.Add(newEvent);
        }   

        static void PrintEventList(List<DayEvent> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("--------------------------------------");
                list[i].PrintEvent();
            }
            Console.WriteLine("--------------------------------------");
        }
    }
}
