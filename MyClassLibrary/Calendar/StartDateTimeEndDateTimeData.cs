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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.