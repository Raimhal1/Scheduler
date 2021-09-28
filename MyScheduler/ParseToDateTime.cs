using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MyScheduler
{
    static class ParseToDateTime
    {
        public static DateTime getDate(string date)
        {
            Regex reg = new Regex(@"^(\d{2}).(\d{2}).(\d{4})$");
            Match match = reg.Match(date);
            reg = new Regex(@"[/]");
            date = reg.Replace(match.Value, ".");
            return  DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        public static void getTime(string time, out int hour, out int minutes)
        {
            var reg = new Regex(@"^(\d+):(\d{2})$");
            Match match = reg.Match(time);
            hour = int.Parse(match.Groups[1].Value);
            minutes = int.Parse(match.Groups[2].Value);
        }
    }
}
