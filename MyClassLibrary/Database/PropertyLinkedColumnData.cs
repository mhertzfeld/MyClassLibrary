using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public class PropertyLinkedColumnData
    {
        //FIELDS
        protected DataColumn dataColumn;

        protected String propertyName;


        //PROPERTIES
        public virtual DataColumn DataColumn
        {
            get { return dataColumn; }

            set
            {
                if (value == default(DataColumn))
                { throw new PropertySetToDefaultException("DataColumn"); }

                dataColumn = value;
            }
        }

        public virtual String PropertyName
        {
            get { return propertyName; }

            set
            {
                if (value == default(String))
                { throw new PropertySetToDefaultException("PropertyName"); }

                propertyName = value;
            }
        }


        //INITIALIZE
        public PropertyLinkedColumnData()
        {
            dataColumn = null;

            propertyName = null;
        }

        public PropertyLinkedColumnData(String PropertyName, DataColumn DataColumn)
        {
            this.DataColumn = DataColumn;

            this.PropertyName = PropertyName;
        }
    }
}