using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;


namespace MyClassLibrary.Calendar
{
    public class MyCalendar
    {
        //METHODS
        public Boolean CheckIfHoliday(DateTime dateTime)
        {
            if ((CheckIfChristmas(dateTime)) || (CheckIfChristmasEve(dateTime)) || (CheckIfEaster(dateTime)) || (CheckIfForthOfJuly(dateTime)) || (CheckIfLaborDay(dateTime)) || (CheckIfMemorialDay(dateTime)) || (CheckIfNewYears(dateTime)) || (CheckIfNewYearsEve(dateTime)) || (CheckIfThanksgiving(dateTime)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Int32 GetBusinessDaysInMonth(Int32 year, Int32 month)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            if ((month < 1) || (month > 12))
            {
                throw new ArgumentOutOfRangeException("month");
            }

            DateTime startDateTime = new DateTime(year, month, 1);

            return GetBusinessDaysInRange(new StartDateTimeEndDateTimeData(startDateTime, GetLastDayOfMonth(startDateTime)));
        }

        public Int32 GetBusinessDaysInRange(StartDateTimeEndDateTimeData startDateTimeEndDateTimeData)
        {
            Int32 result = 0;

            DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime.Value;

            while (dateTime <= startDateTimeEndDateTimeData.EndDateTime)
            {
                if ((dateTime.CheckIfWeekDay()) && (!CheckIfHoliday(dateTime)))
                {
                    result += 1;
                }

                dateTime = dateTime.AddDays(1);
            }

            return result;
        }

        public Int32 GetBusinessDaysInWeek(Int32 year, Int32 week)
        {
            return GetBusinessDaysInWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetBusinessDaysInWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule)
        {
            return GetBusinessDaysInWeek(year, week, calendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetBusinessDaysInWeek(Int32 year, Int32 week, DayOfWeek firstDayOfWeek)
        {
            return GetBusinessDaysInWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        }

        public Int32 GetBusinessDaysInWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            if ((week < 1) || (week > MyCalendar.GetWeekCountForYear(year, calendarWeekRule, firstDayOfWeek)))
            {
                throw new ArgumentOutOfRangeException("week");
            }

            return GetBusinessDaysInRange(GetStartAndEndOfWeek(year, week, calendarWeekRule, firstDayOfWeek));
        }

        public Int32 GetBusinessDaysToDateInMonth(DateTime dateTime)
        {
            return GetBusinessDaysInRange(new StartDateTimeEndDateTimeData(GetFirstDayOfMonth(dateTime), dateTime));
        }

        public Int32 GetBusinessDaysToDateInWeek(DateTime dateTime)
        {
            return GetBusinessDaysToDateInWeek(dateTime, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetBusinessDaysToDateInWeek(DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            return GetBusinessDaysInRange(new StartDateTimeEndDateTimeData(GetFirstDayOfWeek(dateTime, firstDayOfWeek), dateTime));
        }

        public Int32 GetDaysMinusHolidaysInMonth(Int32 year, Int32 month)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            if ((month < 1) || (month > 12))
            {
                throw new ArgumentOutOfRangeException("Month");
            }

            DateTime startDateTime = new DateTime(year, month, 1);

            return GetDaysMinusHolidaysInRange(new StartDateTimeEndDateTimeData(startDateTime, GetLastDayOfMonth(startDateTime)));
        }

        public Int32 GetDaysMinusHolidaysInRange(StartDateTimeEndDateTimeData startDateTimeEndDateTimeData)
        {
            Int32 result = 0;

            DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime.Value;

            while (dateTime <= startDateTimeEndDateTimeData.EndDateTime)
            {
                if (!CheckIfHoliday(dateTime))
                {
                    result += 1;
                }

                dateTime = dateTime.AddDays(1);
            }

            return result;
        }

        public Int32 GetDaysMinusHolidaysInWeek(Int32 year, Int32 week)
        {
            return GetDaysMinusHolidaysInWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetDaysMinusHolidaysInWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule)
        {
            return GetDaysMinusHolidaysInWeek(year, week, calendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetDaysMinusHolidaysInWeek(Int32 year, Int32 week, DayOfWeek firstDayOfWeek)
        {
            return GetDaysMinusHolidaysInWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        }

        public Int32 GetDaysMinusHolidaysInWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            if ((week < 1) || (week > MyCalendar.GetWeekCountForYear(year, calendarWeekRule, firstDayOfWeek)))
            {
                throw new ArgumentOutOfRangeException("week");
            }

            return GetDaysMinusHolidaysInRange(GetStartAndEndOfWeek(year, week, calendarWeekRule, firstDayOfWeek));
        }

        public Int32 GetDaysMinusHolidaysToDateInMonth(DateTime dateTime)
        {
            return GetDaysMinusHolidaysInRange(new StartDateTimeEndDateTimeData(GetFirstDayOfMonth(dateTime), dateTime));
        }

        public Int32 GetDaysMinusHolidaysToDateInWeek(DateTime dateTime)
        {
            return GetDaysMinusHolidaysToDateInWeek(dateTime, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public Int32 GetDaysMinusHolidaysToDateInWeek(DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            return GetDaysMinusHolidaysInRange(new StartDateTimeEndDateTimeData(GetFirstDayOfWeek(dateTime, firstDayOfWeek), dateTime));
        }


        //STATIC METHODS
        public static Boolean CheckIfChristmas(DateTime dateTime)
        {
            return ((dateTime.Month == 12) && (dateTime.Day == 25));
        }

        public static Boolean CheckIfChristmasEve(DateTime dateTime)
        {
            return ((dateTime.Month == 12) && (dateTime.Day == 24));
        }

        public static Boolean CheckIfEaster(DateTime dateTime)
        {
            return (dateTime == GetEasterDate(dateTime.Year));
        }

        public static Boolean CheckIfForthOfJuly(DateTime dateTime)
        {
            return ((dateTime.Month == 7) && (dateTime.Day == 4));
        }

        public static Boolean CheckIfLaborDay(DateTime dateTime)
        {
            return ((dateTime.Month == 9) && (dateTime.DayOfWeek == DayOfWeek.Monday) && (dateTime.Day <= 7));
        }

        public static Boolean CheckIfMemorialDay(DateTime dateTime)
        {
            return ((dateTime.Month == 5) && (Convert.ToByte(dateTime.DayOfWeek) == 2) && (dateTime.Day >= 25));
        }

        public static Boolean CheckIfNewYears(DateTime dateTime)
        {
            return ((dateTime.Month == 1) && (dateTime.Day == 1));
        }

        public static Boolean CheckIfNewYearsEve(DateTime dateTime)
        {
            return ((dateTime.Month == 12) && (dateTime.Day == 31));
        }

        public static Boolean CheckIfThanksgiving(DateTime dateTime)
        {
            return ((dateTime.Month == 11) && (dateTime.DayOfWeek == DayOfWeek.Thursday) && (dateTime.Day >= 22) && (dateTime.Day <= 28));
        }

        public static Boolean CheckIfWeekDay(DateTime dateTime)
        {
            return ((dateTime.DayOfWeek == DayOfWeek.Monday) || (dateTime.DayOfWeek == DayOfWeek.Tuesday) || (dateTime.DayOfWeek == DayOfWeek.Wednesday) || (dateTime.DayOfWeek == DayOfWeek.Thursday) || (dateTime.DayOfWeek == DayOfWeek.Friday));
        }

        public static Boolean CheckIfWeekEnd(DateTime dateTime)
        {
            return ((dateTime.DayOfWeek == DayOfWeek.Saturday) || (dateTime.DayOfWeek == DayOfWeek.Sunday));
        }

        public static DateTime GetDateForDayOfWeekPastOrFuture(DateTime dateTime, Int32 year)
        {
            if (dateTime.Year == year)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            DateTime result = dateTime.AddYears(year - dateTime.Year);

            while (true)
            {
                if (year < dateTime.Year)
                {
                    result = result.AddDays(1);
                }

                if (year > dateTime.Year)
                {
                    result = result.AddDays(-1);
                }

                if (result.DayOfWeek == dateTime.DayOfWeek)
                {
                    return result;
                }
            }
        }

        public static DateTime GetEasterDate(Int32 year)
        {
            //Algorithm below was sourced from http://www.smart.net/~mmontes/ushols.html
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            //Start
            Int32 a = year % 19;

            Int32 b = year / 100;

            Int32 c = year % 100;

            Int32 d = b / 4;

            Int32 e = b % 4;

            Int32 f = (b + 8) / 25;

            Int32 g = (b - f + 1) / 3;

            Int32 h = (19 * a + b - d - g + 15) % 30;

            Int32 i = c / 4;

            Int32 k = c % 4;

            Int32 l = (32 + 2 * e + 2 * i - h - k) % 7;

            Int32 m = (a + 11 * h + 22 * l) / 451;

            Int32 p = (h + l - 7 * m + 114) % 31;

            Int32 month = (h + l - 7 * m + 114) / 31;

            Int32 day = p + 1;
            //End

            return new DateTime(year, month, day);
        }

        public static DateTime GetFirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dateTime)
        {
            return GetFirstDayOfWeek(dateTime, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static DateTime GetFirstDayOfWeek(DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            while (dateTime.DayOfWeek != firstDayOfWeek)
            {
                dateTime = dateTime.AddDays(-1);
            }

            return dateTime;
        }

        public static DateTime GetLastDayOfMonth(DateTime dateTime)
        {
            return GetLastDayOfMonth(dateTime.Year, dateTime.Month);
        }

        public static DateTime GetLastDayOfMonth(Int32 year, Int32 month)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            if ((month < 1) || (month > 12))
            {
                throw new ArgumentOutOfRangeException("Month");
            }
            
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        public static DateTime GetLastDayOfWeek(DateTime dateTime)
        {
            return GetLastDayOfWeek(dateTime, (Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday : Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek - 1));
        }

        public static DateTime GetLastDayOfWeek(DateTime dateTime, DayOfWeek lastDayOfWeek)
        {
            while (dateTime.DayOfWeek != lastDayOfWeek)
            {
                dateTime = dateTime.AddDays(1);
            }

            return dateTime;
        }

        public static Int32 GetQuarter(DateTime dateTime)
        {
            switch (dateTime.Month)
            {
                case 1:

                    return 1;

                case 2:

                    return 1;

                case 3:

                    return 1;

                case 4:

                    return 2;

                case 5:

                    return 2;

                case 6:

                    return 2;

                case 7:

                    return 3;

                case 8:

                    return 3;

                case 9:

                    return 3;

                case 10:

                    return 4;

                case 11:

                    return 4;

                case 12:

                    return 4;

                default:

                    throw new ValueOutOfRangeException("dateTime.Month");
            }
        }

        public static StartDateTimeEndDateTimeData GetStartAndEndOfWeek(Int32 year, Int32 week)
        {
            return GetStartAndEndOfWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static StartDateTimeEndDateTimeData GetStartAndEndOfWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule)
        {
            return GetStartAndEndOfWeek(year, week, calendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static StartDateTimeEndDateTimeData GetStartAndEndOfWeek(Int32 year, Int32 week, DayOfWeek firstDayOfWeek)
        {
            return GetStartAndEndOfWeek(year, week, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        }

        public static StartDateTimeEndDateTimeData GetStartAndEndOfWeek(Int32 year, Int32 week, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            if (week <= 0)
            {
                throw new ArgumentOutOfRangeException("week");
            }

            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            DayOfWeek lastDayOfWeek = (firstDayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Saturday : firstDayOfWeek - 1);

            StartDateTimeEndDateTimeData startDateTimeEndDateTimeData = new Calendar.StartDateTimeEndDateTimeData();

            switch (calendarWeekRule)
            {
                case CalendarWeekRule.FirstDay:

                    Int32 weekCountForYear = Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(2012, 12, 31), CalendarWeekRule.FirstDay, firstDayOfWeek);

                    if (week > weekCountForYear)
                    {
                        throw new ArgumentOutOfRangeException("week");
                    }

                    if (week == 1)
                    {
                        startDateTimeEndDateTimeData.StartDateTime = new DateTime(year, 1, 1);
                    }

                    if (week == weekCountForYear)
                    {
                        startDateTimeEndDateTimeData.EndDateTime = new DateTime(year, 12, 31);
                    }

                    for (DateTime dateTime = new DateTime(year, 1, 1); dateTime <= new DateTime((year + 1), 1, 6); dateTime = dateTime.AddDays(1))
                    {
                        if (Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, firstDayOfWeek) == week)
                        {
                            if (dateTime.DayOfWeek == firstDayOfWeek)
                            {
                                if (startDateTimeEndDateTimeData.StartDateTime == default(DateTime))
                                {
                                    startDateTimeEndDateTimeData.StartDateTime = dateTime;
                                }
                            }

                            if (dateTime.DayOfWeek == lastDayOfWeek)
                            {
                                if (startDateTimeEndDateTimeData.EndDateTime == default(DateTime))
                                {
                                    startDateTimeEndDateTimeData.EndDateTime = dateTime;
                                }

                                break;
                            }
                        }
                    }

                    break;

                case CalendarWeekRule.FirstFourDayWeek:

                    if ((week < 1) || (week > 52))
                    {
                        throw new ArgumentOutOfRangeException("week");
                    }

                    DateTime firstDayOfYearDateTime = new DateTime(year, 1, 1);

                    Int32 dayCount = 0;

                    for (DateTime dateTime = new DateTime(year, 1, 1); dateTime.DayOfYear <= 4; dateTime = dateTime.AddDays(1))
                    {
                        dayCount += 1;

                        if (dateTime.DayOfWeek == lastDayOfWeek)
                        {
                            break;
                        }
                    }

                    if (dayCount < 4)
                    {
                        startDateTimeEndDateTimeData.StartDateTime = firstDayOfYearDateTime.AddDays(7 - (Int32)firstDayOfYearDateTime.DayOfWeek);
                    }
                    else
                    {
                        startDateTimeEndDateTimeData.StartDateTime = firstDayOfYearDateTime.AddDays(((Int32)firstDayOfYearDateTime.DayOfWeek) * -1);
                    }

                    for (DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime.Value.AddDays(1); dateTime <= new DateTime(year, 12, 31); dateTime = dateTime.AddDays(1))
                    {
                        if (Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek) == week)
                        {
                            if (dateTime.DayOfWeek == firstDayOfWeek)
                            {
                                startDateTimeEndDateTimeData.StartDateTime = dateTime;
                            }

                            if (dateTime.DayOfWeek == lastDayOfWeek)
                            {
                                startDateTimeEndDateTimeData.EndDateTime = dateTime;

                                break;
                            }
                        }
                    }

                    break;

                case CalendarWeekRule.FirstFullWeek:

                    if ((week < 1) || (week > Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(2012, 12, 31), CalendarWeekRule.FirstFullWeek, firstDayOfWeek)))
                    {
                        throw new ArgumentOutOfRangeException("week");
                    }

                    for (DateTime dateTime = new DateTime(year, 1, 1); dateTime <= new DateTime((year + 1), 1, 6); dateTime = dateTime.AddDays(1))
                    {
                        if (Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFullWeek, firstDayOfWeek) == week)
                        {
                            if (dateTime.DayOfWeek == firstDayOfWeek)
                            {
                                startDateTimeEndDateTimeData.StartDateTime = dateTime;
                            }

                            if (dateTime.DayOfWeek == lastDayOfWeek)
                            {
                                if (startDateTimeEndDateTimeData.StartDateTime != default(DateTime))
                                {
                                    startDateTimeEndDateTimeData.EndDateTime = dateTime;

                                    break;
                                }
                            }
                        }
                    }

                    break;

                default:

                    throw new ValueOutOfRangeException("calendarWeekRule");
            }

            return startDateTimeEndDateTimeData;
        }

        public static Int32 GetWeekCountForYear(Int32 year)
        {
            return GetWeekCountForYear(year, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static Int32 GetWeekCountForYear(Int32 year, CalendarWeekRule calendarWeekRule)
        {
            return GetWeekCountForYear(year, calendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static Int32 GetWeekCountForYear(Int32 year, DayOfWeek firstDayOfWeek)
        {
            return GetWeekCountForYear(year, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        }

        public static Int32 GetWeekCountForYear(Int32 year, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException("year");
            }

            return Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(new DateTime(year, 12, 31), calendarWeekRule, firstDayOfWeek);
        }

        public static Int32 GetWeekOfYear(DateTime dateTime)
        {
            return GetWeekOfYear(dateTime, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static Int32 GetWeekOfYear(DateTime dateTime, CalendarWeekRule calendarWeekRule)
        {
            return GetWeekOfYear(dateTime, calendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public static Int32 GetWeekOfYear(DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            return GetWeekOfYear(dateTime, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
        }

        public static Int32 GetWeekOfYear(DateTime dateTime, CalendarWeekRule calendarWeekRule, DayOfWeek firstDayOfWeek)
        {
            return Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(dateTime, calendarWeekRule, firstDayOfWeek);
        }
    }
}