using MyClassLibrary.Calendar;
using System;


namespace MyClassLibrary.Calendar
{
    public static class DateTimeExtensions
    {
        public static Boolean CheckIfWeekDay(this DateTime dateTime)
        {
            return MyCalendar.CheckIfWeekDay(dateTime);
        }

        public static Boolean CheckIfWeekEnd(this DateTime dateTime)
        {
            return MyCalendar.CheckIfWeekEnd(dateTime);
        }

        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return MyCalendar.GetFirstDayOfMonth(dateTime);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
        {
            return MyCalendar.GetFirstDayOfWeek(dateTime);
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return MyCalendar.GetLastDayOfMonth(dateTime);
        }

        public static DateTime GetLastDayOfWeek(this DateTime dateTime)
        {
            return MyCalendar.GetLastDayOfWeek(dateTime);
        }

        public static String GetMonthName(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM");
        }

        public static Int32 GetQuarter(this DateTime dateTime)
        {
            return MyCalendar.GetQuarter(dateTime);
        }

        public static Int32 GetWeekOfYear(this DateTime dateTime)
        {
            return MyCalendar.GetWeekOfYear(dateTime);
        }
    }
}