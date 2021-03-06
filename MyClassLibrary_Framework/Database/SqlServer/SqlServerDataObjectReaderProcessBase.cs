﻿using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectReaderProcessBase<T_DataObject>
        : Database.DataObjectReaderProcessBase<T_DataObject, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
    {

    }
}