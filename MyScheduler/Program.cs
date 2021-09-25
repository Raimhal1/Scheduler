using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        private static string CreateDirectory()
        {
            var currentPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var directoryPath = currentPath + "Event_lists";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }

        static async void WriteEventListToFile(List<DayEvent> list)
        {
            var directoryPath = CreateDirectory();
            int count = Directory.GetFiles(directoryPath).Length;
            var filePath = directoryPath + $"\\event_list_{count}" + ".txt";

            using (StreamWriter file = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                foreach (DayEvent Event in list)
                {
                    await file.WriteLineAsync(Event.getInfo());
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"The event list wrote to the {Path.GetFileName(filePath)}");
            Console.ResetColor();

        }
        static void CreateDate(out DateTime date, string state = "start")
        {
            try
            {
                Console.Write($"Enter event {state} date : ");
                var str = Console.ReadLine();
                Regex reg = new Regex(@"^(\d{2}).(\d{2}).(\d{4})$");
                Match match = reg.Match(str);
                reg = new Regex(@"[/]");
                str = reg.Replace(match.Value, ".");

                var check = DateTime.ParseExact(str, "dd.MM.yyyy", CultureInfo.InvariantCulture);

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

                var reg = new Regex(@"^(\d+):(\d{2})$");
                Match match = reg.Match(str);

                if (!match.Success) { throw new FormatException(); }

                int hours = int.Parse(match.Groups[1].Value), minutes = int.Parse(match.Groups[2].Value);
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
        static void CreateEventName(List<DayEvent> list, out string finalEventName)
        {
            string eventName = "";
            try
            {
                Console.Write("Enter event name : ");
                eventName = Console.ReadLine();
                if (eventName.Length == 0) { throw new InvalidOperationException(); }
                if (list.Exists((Event) => Event.eventName == eventName)) { throw new ArgumentException(); }
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

        static void RemoveEventFromEventList(List<DayEvent> list, string name)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var someEvent = list.Find(Event => Event.eventName == name);
            if (someEvent != null)
            {
                list.Remove(someEvent);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Event {name} deleted");
            }
            else { Console.WriteLine($"Event {name} not exists"); }
            Console.ResetColor();
        }

        static void SortEventListByEventStartDate(List<DayEvent> list)
        {
            list.Sort((x, y) => x.StartEventDate > y.StartEventDate ? 1 : -1);
            PrintEventList(list);
        }

        static void PrintEventList(List<DayEvent> list)
        {
            if (list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Event list is empty!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Event list : ");
            Console.ResetColor();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("--------------------------------------");
                list[i].PrintEvent();
            }
            Console.WriteLine("--------------------------------------");
        }

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
                Console.WriteLine("Enter 3 - Sort and print event list");
                Console.WriteLine("Enter 4 - Print event list");
                Console.WriteLine("Enter 5 - Clear event list");
                Console.WriteLine("Enter 6 - Write event list to file");
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
                        SortEventListByEventStartDate(events);
                        break;
                    case 4:
                        PrintEventList(events);
                        break;
                    case 5:
                        events.Clear();
                        break;
                    case 6:
                        WriteEventListToFile(events);
                        break;
                    case 7:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Console cleared");
                        Console.ResetColor();
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
    }
}
