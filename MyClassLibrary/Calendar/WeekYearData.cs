using System;


namespace MyClassLibrary.Calendar
{
    public class YearWeekData
    {
        //FIELDS
        protected Int32 week;

        protected Int32 year;


        //PROPERTIES
        public Int32 Week
        {
            get { return week; }

            set
            {
                if ((value < 1) || (value > 53))
                {
                    throw new ValueOutOfRangeException("Week");
                }

                week = value;
            }
        }

        public Int32 Year
        {
            get { return year; }

            protected set
            {
                if (value < 1)
                {
                    throw new ValueOutOfRangeException("Year");
                }

                year = value;
            }
        }


        //INITIALIZE
        public YearWeekData()
        {
            week = 0;

            year = 0;
        }

        public YearWeekData(Int32 Year, Int32 Week)
        {
            this.Week = Week;

            this.Year = Year;
        }
    }
}
