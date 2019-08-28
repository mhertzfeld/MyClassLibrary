using System;
using System.Collections.Generic;


namespace MyClassLibrary
{
    public class DateRange
    {
        #region STATIC METHODS
        public static IEnumerable<DateTime> InterateByDate(DateRange _DateRange)
        {
            return InterateByDate(_DateRange.Start_DateTime.Value, _DateRange.End_DateTime.Value);
        }

        public static IEnumerable<DateTime> InterateByDate(DateTime _Start_DateTime, DateTime _End_DateTime)
        {
            for (DateTime _DateTime = _Start_DateTime; _DateTime <= _End_DateTime; _DateTime = _DateTime.AddDays(1))
            { yield return _DateTime; }
        }
        #endregion

        #region FIELDS
        protected DateTime? _End_DateTime;

        protected DateTime? _Start_DateTime;
        #endregion

        #region PROPERTIES
        public virtual DateTime? End_DateTime
        {
            get { return _End_DateTime; }

            set
            {
                if (value == default(DateTime?))
                {
                    throw new PropertySetToDefaultException("End_DateTime");
                }

                _End_DateTime = value;
            }
        }

        public virtual DateTime? Start_DateTime
        {
            get { return _Start_DateTime; }

            set
            {
                if (value == default(DateTime?))
                {
                    throw new PropertySetToDefaultException("Start_DateTime");
                }

                _Start_DateTime = value;
            }
        }

        public virtual TimeSpan TimeSpan
        {
            get
            {
                if (End_DateTime == null)
                { throw new Exception("EndDateTime cannot be null."); }

                if (Start_DateTime == null)
                { throw new Exception("StartDateTime cannot be null."); }

                return (End_DateTime.GetValueOrDefault() - Start_DateTime.GetValueOrDefault());
            }
        }
        #endregion

        #region CONSTRUCTOR
        public DateRange()
        {
            _End_DateTime = null;

            _Start_DateTime = null;
        }

        public DateRange(DateTime Start_DateTime, DateTime End_DateTime)
        {
            this.End_DateTime = End_DateTime;

            this.Start_DateTime = Start_DateTime;
        }
        #endregion
    }
}