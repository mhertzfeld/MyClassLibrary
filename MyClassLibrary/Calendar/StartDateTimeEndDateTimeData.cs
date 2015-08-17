using System;
using System.Collections.Generic;


namespace MyClassLibrary.Calendar
{
    public class StartDateTimeEndDateTimeData
    {
        //STATIC METHODS
        public static IEnumerable<DateTime> InterateByDate(StartDateTimeEndDateTimeData startDateTimeEndDateTimeData)
        {
            for (DateTime dateTime = startDateTimeEndDateTimeData.StartDateTime; dateTime <= startDateTimeEndDateTimeData.EndDateTime; dateTime = dateTime.AddDays(1))
            { yield return dateTime; }
        }


        //FIELDS
        protected DateTime endDateTime;

        protected DateTime startDateTime;


        //PROPERTIES
        public virtual DateTime EndDateTime
        {
            get { return endDateTime; }

            set
            {
                if (value == default(DateTime))
                {
                    throw new PropertySetToDefaultException("EndDate");
                }

                endDateTime = value;
            }
        }

        public virtual DateTime StartDateTime
        {
            get { return startDateTime; }

            set
            {
                if (value == default(DateTime))
                {
                    throw new PropertySetToDefaultException("StartDate");
                }

                startDateTime = value;
            }
        }

        public virtual TimeSpan TimeSpan
        {
            get { return (EndDateTime - StartDateTime); }
        }


        //INITIALIZE
        public StartDateTimeEndDateTimeData()
        {
            endDateTime = default(DateTime);

            startDateTime = default(DateTime);
        }

        public StartDateTimeEndDateTimeData(DateTime StartDateTime, DateTime EndDateTime)
        {
            this.EndDateTime = EndDateTime;

            this.StartDateTime = StartDateTime;
        }


        //METHODS
        public IEnumerable<DateTime> InterateByDate()
        {
            return InterateByDate(this);
        }

        public Boolean Validate()
        {
            return !(StartDateTime > EndDateTime);
        }
    }
}