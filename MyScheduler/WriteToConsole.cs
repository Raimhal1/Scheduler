using System;
using System.Collections.Generic;
using System.Text;

namespace MyScheduler
{
    class WriteToConsole : IWriteInfo
    {

        public void Write(List<DayEvent> list)
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
                WriteEvent(list[i]);
            }
            Console.WriteLine("--------------------------------------");
        }

        static void WriteEvent(DayEvent Event)
        {
            var info = Event.getInfo();
            Console.Write("Event name : ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{ info.Item1}");
            Console.ResetColor();
            Console.WriteLine($"Start event : {info.Item2:f}\nEnd event : {info.Item3:f}\n" +
                $"Event duration : {info.Item4}");
            if (info.Item5.Length != 0) { Console.WriteLine($"Description : {info.Item5}"); }
        }
    }
}
