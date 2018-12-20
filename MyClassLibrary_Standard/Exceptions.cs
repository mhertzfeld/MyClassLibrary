using System;

namespace MyClassLibrary
{
    public class PropertySetToDefaultException
    : System.Exception
    {
        public PropertySetToDefaultException(String propertyName)
            : base("'" + propertyName + "' can not be set to its default.")
        { }
    }

    public class PropertySetToOutOfRangeValueException
    : System.Exception
    {
        public PropertySetToOutOfRangeValueException(String propertyName)
            : base("'" + propertyName + "' can not be set to an out of range vale.")
        { }
    }

    public class ValueOutOfRangeException
    : System.Exception
    {
        public ValueOutOfRangeException(String variableName)
            : base("The value for '" + variableName + "' is out of range.")
        { }
    }
}