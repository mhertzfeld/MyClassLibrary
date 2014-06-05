using System;


namespace MyClassLibrary.Calendar
{
    public class StartDateTimeEndDateTimeData
    {
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
        public Boolean Validate()
        {
            return !(StartDateTime > EndDateTime);
        }
    }
}