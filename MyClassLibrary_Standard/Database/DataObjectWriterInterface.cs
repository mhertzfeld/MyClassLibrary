using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public interface DataObjectWriterInterface
    {
        T_DbCommand CreateDbCommand<T_DbCommand>(IDbConnection dbConnection)
            where T_DbCommand : IDbCommand, new();
    }
}