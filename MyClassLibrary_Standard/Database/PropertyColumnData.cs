using System;


namespace MyClassLibrary.Database
{
    public class PropertyColumnData
    {
        //FIELDS
        protected String _ColumnName;

        protected Type _DataType;

        protected String _PropertyName;


        //PROPERTIES
        public virtual String ColumnName
        {
            get { return _ColumnName; }

            set
            {
                if (value == default(String))
                { throw new PropertySetToDefaultException("DataColumn"); }

                _ColumnName = value;
            }
        }

        public virtual Type DataType
        {
            get { return _DataType; }

            set
            {
                if (value == default(Type))
                { throw new PropertySetToDefaultException("DataType"); }

                _DataType = value;
            }
        }

        public virtual String PropertyName
        {
            get { return _PropertyName; }

            set
            {
                if (value == default(String))
                { throw new PropertySetToDefaultException("PropertyName"); }

                _PropertyName = value;
            }
        }


        //INITIALIZE
        public PropertyColumnData()
        {
            _ColumnName = null;

            _DataType = null;
            
            _PropertyName = null;
        }

        public PropertyColumnData(String PropertyName, String ColumnName, Type DataType)
        {
            this.ColumnName = ColumnName;

            this.DataType = DataType;

            this.PropertyName = PropertyName;
        }
    }
}