using System;


namespace MyClassLibrary
{
    public class PropertySetToDefaultException
        : System.Exception
    {
        public PropertySetToDefaultException(String propertyName)
            : base("'" + propertyName + "' can not be set to its default.")
        {
            
        }
    }
}