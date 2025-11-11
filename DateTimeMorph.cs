using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Internship
{
    public enum MorphPeriod
    {
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }

    public class DateTimeMorph
    {
        public MorphPeriod Period { get; set; } = MorphPeriod.Day;
        public DateTime DateTime { get; set; } = DateTime.Now;

        public DateTime PeriodStart
        {
            get

            {
                switch (Period)

                {

                    case MorphPeriod.Second:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, DateTime.Second);
                    case MorphPeriod.Minute:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, 0);
                    case MorphPeriod.Hour:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, 0, 0);
                    case MorphPeriod.Day:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day);
                    case MorphPeriod.Week:
                        DayOfWeek startOfWeek = DayOfWeek.Sunday;
                        int diff = (7 + (DateTime.DayOfWeek - startOfWeek)) % 7;
                        return DateTime.AddDays(-1 * diff).Date;
                    case MorphPeriod.Month:
                        return new DateTime(DateTime.Year, DateTime.Month, 1);
                    case MorphPeriod.Quarter:
                        var month = DateTime.Month - (DateTime.Month - 1) % 3;
                        var result = new DateTime(DateTime.Year, month, 1);
                        return result;
                    case MorphPeriod.Year:
                        return new DateTime(DateTime.Year, 1, 1);

                    default:
                        return DateTime;

                }

            }

        }

        public DateTime PeriodEnd
        {
            get

            {
                switch (Period)
                {

                    case MorphPeriod.Second:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, DateTime.Second);
                    case MorphPeriod.Minute:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, DateTime.Minute, 59);
                    case MorphPeriod.Hour:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, DateTime.Hour, 59, 59);
                    case MorphPeriod.Day:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.Day, 23, 59, 59);
                    case MorphPeriod.Week:
                        DayOfWeek startOfWeek = DayOfWeek.Sunday;
                        int diff = (7 + (DateTime.DayOfWeek - startOfWeek)) % 7;
                        DateTime start = DateTime.AddDays(-1 * diff).Date;
                        int otherstart = start.Day;
                        DateTime end = start.AddDays(6);
                        int otherend = otherstart + 6;
                        return new DateTime(DateTime.Year, DateTime.Month, otherend, 23, 59, 59);
                    case MorphPeriod.Month:
                        return new DateTime(DateTime.Year, DateTime.Month, DateTime.DaysInMonth(DateTime.Year, DateTime.Month), 23, 59, 59);
                    case MorphPeriod.Quarter:
                        var month = DateTime.Month - (DateTime.Month - 1) % 3;
                        DateTime last = new DateTime(DateTime.Year, month, 1, 23, 59, 59);
                        var lastday = last.AddMonths(3).AddDays(-1);
                        return lastday;
                    case MorphPeriod.Year:
                        return new DateTime(DateTime.Year, 12, 31, 23, 59, 59);

                    default:
                        return DateTime;

                }

            }
        }
        public DateTimeMorph()
        {

        }

        public DateTimeMorph(DateTime initialDateTime)
        {
            DateTime = initialDateTime;
        }

        public void Next()
        {
            if (Period == MorphPeriod.Second)
            {
                DateTime = DateTime.AddSeconds(1);
            }
            else if (Period == MorphPeriod.Minute)
            {
                DateTime = DateTime.AddMinutes(1);
            }
            else if (Period == MorphPeriod.Hour)
            {
                DateTime = DateTime.AddHours(1);
            }
            else if (Period == MorphPeriod.Day)
            {
                DateTime = DateTime.AddDays(1);
            }
            else if (Period == MorphPeriod.Week)
            {
                DateTime = DateTime.AddDays(7);
            }
            else if (Period == MorphPeriod.Month)
            {
                DateTime = DateTime.AddMonths(1);
            }
            else if (Period == MorphPeriod.Quarter)
            {
                DateTime = DateTime.AddMonths(3);
            }
            else if (Period == MorphPeriod.Year)
            {
                DateTime = DateTime.AddYears(1);
            }


        }

        public void Previous()
        {
            if (Period == MorphPeriod.Second)
            {
                DateTime = DateTime.AddSeconds(-1);
            }
            else if (Period == MorphPeriod.Minute)
            {
                DateTime = DateTime.AddMinutes(-1);
            }
            else if (Period == MorphPeriod.Hour)
            {
                DateTime = DateTime.AddHours(-1);
            }
            else if (Period == MorphPeriod.Day)
            {
                DateTime = DateTime.AddDays(-1);
            }
            else if (Period == MorphPeriod.Week)
            {
                DateTime = DateTime.AddDays(-7);
            }
            else if (Period == MorphPeriod.Month)
            {
                DateTime = DateTime.AddMonths(-1);
            }
            else if (Period == MorphPeriod.Quarter)
            {
                DateTime = DateTime.AddMonths(-3);
            }
            else if (Period == MorphPeriod.Year)
            {
                DateTime = DateTime.AddYears(-1);
            }

        }

        public void Add(Int32 interval)
        {
            if (Period == MorphPeriod.Second)
            {
                DateTime = DateTime.AddSeconds(interval);
            }
            else if (Period == MorphPeriod.Minute)
            {
                DateTime = DateTime.AddMinutes(interval);
            }
            else if (Period == MorphPeriod.Hour)
            {
                DateTime = DateTime.AddHours(interval);
            }
            else if (Period == MorphPeriod.Day)
            {
                DateTime = DateTime.AddDays(interval);
            }
            else if (Period == MorphPeriod.Week)
            {
                DateTime = DateTime.AddDays(interval * 7);
            }
            else if (Period == MorphPeriod.Month)
            {
                DateTime = DateTime.AddMonths(interval);
            }
            else if (Period == MorphPeriod.Quarter)
            {
                DateTime = DateTime.AddMonths(interval * 3);
            }
            else if (Period == MorphPeriod.Year)
            {
                DateTime = DateTime.AddYears(interval);
            }
        }

        public void Subtract(Int32 interval)
        {
            if (Period == MorphPeriod.Second)
            {
                DateTime = DateTime.AddSeconds(-(interval));
            }
            else if (Period == MorphPeriod.Minute)
            {
                DateTime = DateTime.AddMinutes(-(interval));
            }
            else if (Period == MorphPeriod.Hour)
            {
                DateTime = DateTime.AddHours(-(interval));
            }
            else if (Period == MorphPeriod.Day)
            {
                DateTime = DateTime.AddDays(-(interval));
            }
            else if (Period == MorphPeriod.Week)
            {
                DateTime = DateTime.AddDays(-(interval * 7));
            }
            else if (Period == MorphPeriod.Month)
            {
                DateTime = DateTime.AddMonths(-(interval));
            }
            else if (Period == MorphPeriod.Quarter)
            {
                DateTime = DateTime.AddMonths(-(interval * 3));
            }
            else if (Period == MorphPeriod.Year)
            {
                DateTime = DateTime.AddYears(-(interval));
            }
        }

        public Boolean DownPeriod()
        {
            if (Period == MorphPeriod.Second)
            {
                return false;
            }

            Period = Period - 1;

            return true;
        }

        public Boolean UpPeriod()
        {
            if (Period == MorphPeriod.Second)
            {
                return false;
            }

            Period = Period + 1;

            return true;
        }

        public Boolean ParseText(String text)
        {

            string[] periods = { "Second", "Minute", "Hour", "Day", "Week", "Month", "Quarter", "Year" };
            string[] daysofweek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            for (int i = 0; i < periods.Length; ++i)
            {
                if (text.IndexOf(periods[i], StringComparison.OrdinalIgnoreCase) > -1)
                {
                    Period = (MorphPeriod)i;
                    break;
                }
            }

            DayOfWeek mentionedday;

            for (int i = 0; i < daysofweek.Length; ++i)
            {
                if (text.IndexOf(daysofweek[i], StringComparison.OrdinalIgnoreCase) > -1)
                {
                    mentionedday = (DayOfWeek)i;

                    if (text.Contains("Next", StringComparison.OrdinalIgnoreCase))
                    {
                        int daysToAdd = (((int)mentionedday - (int)DateTime.Today.DayOfWeek + 7) % 7);
                        if (daysToAdd == 0)
                        {
                            daysToAdd = 7;
                        }
                        Add(daysToAdd + 1);
                        return true;
                    }
                    else if (text.Contains("Last", StringComparison.OrdinalIgnoreCase))
                    {
                        int currentDay = (int)DateTime.Today.DayOfWeek, gotoDay = (int)mentionedday;
                        DateTime = DateTime.Today.AddDays(-6).AddDays(gotoDay - currentDay);
                        return true;
                    }
                }
            }

            if (text.Contains("Next", StringComparison.OrdinalIgnoreCase) || text.Contains("tomorrow", StringComparison.OrdinalIgnoreCase))
            {
                Next();
                return true;
            }

            else if (text.Contains("Last", StringComparison.OrdinalIgnoreCase) || text.Contains("yesterday", StringComparison.OrdinalIgnoreCase))
            {
                Previous();
                return true;
            }

            else if (text.Contains("Ago", StringComparison.OrdinalIgnoreCase))
            {
                string resultstring = Regex.Match(text, @"\d+").Value;
                int interval = Int32.Parse(resultstring);
                Subtract(interval);
                return true;
            }

            else if (text.Contains("In", StringComparison.OrdinalIgnoreCase))
            {
                string resultstring = Regex.Match(text, @"\d+").Value;
                int interval = Int32.Parse(resultstring);
                Add(interval);
                return true;
            }

            else if (DateTime.TryParse(text, out DateTime val) == true)
            {
                DateTime = val;
                DateTime.Parse(text);
                return true;
            }

            else if (DateTime.TryParseExact(text, "MMMM", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                int month = DateTime.ParseExact(text, "MMMM", CultureInfo.CurrentCulture).Month;
                DateTime = new DateTime(DateTime.Year, month, 1);
                return true;
            }

            return false;

        }
    }
}