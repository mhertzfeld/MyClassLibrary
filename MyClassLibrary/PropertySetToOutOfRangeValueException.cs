using System;


namespace MyClassLibrary
{
    public class PropertySetToOutOfRangeValueException
        : System.Exception
    {
        public PropertySetToOutOfRangeValueException(String propertyName)
            : base("'" + propertyName + "' can not be set to an out of range vale.")
        {
            
        }
    }
}