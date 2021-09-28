using System;
using System.Collections.Generic;
using System.IO;

namespace MyScheduler
{

    class Program
    {
        public static List<DayEvent> events;

        static void Main(string[] args)
        {
            events = new List<DayEvent>();
            events.Add(new DayEvent("event1", new DateTime(2010, 12, 12, 6, 20, 0), new DateTime(2020, 12, 12, 9, 30, 0)));
            events.Add(new DayEvent("event2", new DateTime(2001, 5, 1, 8, 30, 0), new DateTime(2010, 2, 10, 2, 30, 0)));
            events.Add(new DayEvent("event3", new DateTime(2007, 6, 2, 1, 30, 0), new DateTime(2008, 5, 12, 8, 30, 0)));
            Menu(events);

        }

        // user interface
        static void Menu(List<DayEvent> events)
        {
            
            while (true)
            {
                bool end = false;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Menu : ");
                Console.ResetColor();
                Console.WriteLine("Enter 1 - Create new event");
                Console.WriteLine("Enter 2 - Remove some event");
                Console.WriteLine("Enter 3 - Sort  event list");
                Console.WriteLine("Enter 4 - Write event list");
                Console.WriteLine("Enter 5 - Write event list to file");
                Console.WriteLine("Enter 6 - Clear event list");
                Console.WriteLine("Enter 7 - Clear console");
                Console.WriteLine("Enter 0 - Exit");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Choose an action : ");
                Console.ResetColor();

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice > 8 || choice < 0)
                {
                    Console.WriteLine("Not correct input! Try again\n\n--------\n\n");
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        end = true;
                        break;
                    case 1:
                        CreateNewEvent(events);
                        break;
                    case 2:
                        Console.Write("Enter the name of the event that you want to delete : ");
                        RemoveEventFromEventList(events, Console.ReadLine());
                        break;
                    case 3:
                        SortEventListByStarEventtDate(events);
                        break;
                    case 4:
                        WriteEventListToConsole(events);
                        break;
                    case 5:
                        WriteEventListToFile(events);
                        break;
                    case 6:
                        events.Clear();
                        break;
                    case 7:
                        ClearConsole();
                        break;
                    default:
                        break;
                }

                if (end)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Good luck!");
                    Console.ResetColor();
                    break;
                }
            }

        }


        // create date and time
        static void CreateDate(out DateTime date, string state = "start")
        {
            try
            {
                Console.Write($"Enter event {state} date : ");
                var str = Console.ReadLine();
                var check = ParseToDateTime.getDate(str);
                date = check;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Date is not in the proper format. Try again!");
                Console.ResetColor();
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
                int hours, minutes;
                ParseToDateTime.getTime( str, out hours, out minutes);
                date_and_time = new DateTime(date.Year, date.Month, date.Day, hours, minutes, 0);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Time is not in the proper format. Try again!");
                Console.ResetColor();
                CreateTime(in date, out date_and_time, state);
                return;
            }
        }

        // create event name
        static void CreateEventName(List<DayEvent> list, out string finalEventName)
        {
            string eventName = "";
            try
            {
                Console.Write("Enter event name : ");
                eventName = Console.ReadLine();
                if (eventName.Length == 0) { throw new InvalidOperationException(); }
                if (list.Exists((Event) => Event.EventName == eventName)) { throw new ArgumentException(); }
                finalEventName = eventName;
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The event needs a name! Try again!");
                Console.ResetColor();
                CreateEventName(list, out finalEventName);
                return;
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The event has just exist! Try again!");
                Console.ResetColor();
                CreateEventName(list, out finalEventName);
                return;
            }
        }

        // create new event
        static void CreateNewEvent(List<DayEvent> list)
        {
            // set event name
            string eventName;
            CreateEventName(list, out eventName);

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
                if (eventEndDate < eventStartDate) { throw new InvalidOperationException(); }
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Such an event cannot exist!\n" +
                    "The end date of the event must be later than the start date of the event!");
                Console.WriteLine("Try again to create an event.");
                Console.ResetColor();
                CreateNewEvent(list);
                return;
            }

            // set event description
            Console.Write("Enter event description : ");
            string description = Console.ReadLine();

            // create new event
            DayEvent newEvent = new DayEvent(eventName, eventStartDate, eventEndDate, description);
            events.Add(newEvent);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Event created");
            Console.ResetColor();
        }

        // delete some event
        static void RemoveEventFromEventList(List<DayEvent> list, string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var someEvent = list.Find(Event => Event.EventName == name);
            if (someEvent != null)
            {
                list.Remove(someEvent);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Event {name} deleted");
            }
            else { Console.WriteLine($"Event {name} not exists"); }
            Console.ResetColor();
        }

        // sort event list
        static void SortEventListByStarEventtDate(List<DayEvent> list)
        {
            list.Sort((x, y) => x.StartEventDate > y.StartEventDate ? 1 : -1);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Event list sorted!");
            Console.ResetColor();
        }


        // write event list to console
        static void WriteEventListToConsole(List<DayEvent> events)
        {
            IWriteInfo console = new WriteToConsole();
            console.Write(events);
        }

        // write event list to file
        static void WriteEventListToFile(List<DayEvent> Events)
        {
            // message
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"The event list wrote to the " +
                $"{Path.GetFileName(WriteToFile.CreateFilePath())}");
            Console.ResetColor();

            // write to file
            IWriteInfo write = new WriteToFile();
            write.Write(Events);
        }


        // clear console 
        static void ClearConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Console cleared");
            Console.ResetColor();
        }
    }
}
