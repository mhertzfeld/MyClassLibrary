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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.