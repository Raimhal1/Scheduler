using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyScheduler
{
    public class WriteToFile : IWriteInfo
    {
        // write to file
        public async void Write(List<DayEvent> Events)
        {
            var filePath = CreateFilePath();

            using (StreamWriter file = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                foreach (DayEvent Event in Events)
                {
                    await file.WriteLineAsync(ConvertEventInfoToString(Event));
                }
            }
        }

        // converting information to string
        static string ConvertEventInfoToString(DayEvent Event)
        {
            var info = Event.getInfo();
            string finalString =
                $"Event name : {info.Item1}\nStart event : {info.Item2:f}\n" +
                $"End event : {info.Item3:f}\nEvent duration : {info.Item4}";

            if (info.Item5 != "") { finalString += $"\nDescription : {info.Item5}"; }

            finalString += "\n";
            return finalString;
        }

        // create directory 
        private static string CreateDirectoryPath()
        {
            var currentPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var directoryPath = currentPath + "Event_lists";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }

        // find file path
        public static string CreateFilePath()
        {
            var directoryPath = CreateDirectoryPath();
            int count = Directory.GetFiles(directoryPath).Length;
            var filePath = directoryPath + $"\\event_list_{count}" + ".txt";
            return filePath;
        }
    }
}
