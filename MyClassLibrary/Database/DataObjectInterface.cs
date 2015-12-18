using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public interface DataObjectInterface
    {
        void SetFields(IDataReader _IDataReader);
    }
}