using System;
using System.Collections.Generic;


namespace MyClassLibrary.Calendar
{
    public static class StartDateTimeEndDateTimeDataExtensions
    {
        public static IEnumerable<DateTime> InterateByDate(this StartDateTimeEndDateTimeData startDateTimeEndDateTimeData)
        {
            for (DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime; dateTime <= startDateTimeEndDateTimeData.EndDateTime; dateTime = dateTime.AddDays(1))
            { yield return dateTime; }
        }
    }
}