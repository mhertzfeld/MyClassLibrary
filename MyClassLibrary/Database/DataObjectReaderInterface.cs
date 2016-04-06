using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public interface DataObjectReaderInterface
    {
        void SetFields(IDataReader _IDataReader);
    }
}