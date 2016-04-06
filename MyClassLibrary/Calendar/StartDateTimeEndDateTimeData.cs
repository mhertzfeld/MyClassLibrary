using System;
using System.Collections.Generic;


namespace MyClassLibrary.Calendar
{
    public class StartDateTimeEndDateTimeData
    {
        //STATIC METHODS
        public static IEnumerable<DateTime> InterateByDate(StartDateTimeEndDateTimeData startDateTimeEndDateTimeData)
        {
            for (DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime.Value; dateTime <= startDateTimeEndDateTimeData.EndDateTime.Value; dateTime = dateTime.AddDays(1))
            { yield return dateTime; }
        }


        //FIELDS
        protected DateTime? endDateTime;

        protected DateTime? startDateTime;


        //PROPERTIES
        public virtual DateTime? EndDateTime
        {
            get { return endDateTime; }

            set
            {
                if (value == default(DateTime?))
                {
                    throw new PropertySetToDefaultException("EndDate");
                }

                endDateTime = value;
            }
        }

        public virtual DateTime? StartDateTime
        {
            get { return startDateTime; }

            set
            {
                if (value == default(DateTime?))
                {
                    throw new PropertySetToDefaultException("StartDate");
                }

                startDateTime = value;
            }
        }

        public virtual TimeSpan TimeSpan
        {
            get
            {
                if (EndDateTime == null)
                { throw new Exception("EndDateTime cannot be null."); }

                if (StartDateTime == null)
                { throw new Exception("StartDateTime cannot be null."); }

                return (EndDateTime.GetValueOrDefault() - StartDateTime.GetValueOrDefault());
            }
        }


        //INITIALIZE
        public StartDateTimeEndDateTimeData()
        {
            endDateTime = null;

            startDateTime = null;
        }

        public StartDateTimeEndDateTimeData(DateTime StartDateTime, DateTime EndDateTime)
        {
            this.EndDateTime = EndDateTime;

            this.StartDateTime = StartDateTime;
        }
    }
}