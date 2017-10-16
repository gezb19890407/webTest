using System;
using System.Collections.Generic;
using System.Data;

namespace System.Data
{
    public interface ICommandCustomHelper
    {
        DatabaseTypeCustom DatabaseTypeCustom { get; }

        bool isCanConnection(string connectionString);

        bool isCanConnection();

        string checkParam(string param);

        string joinToSqlString<T>(List<T> tList);

        //string convertToSqlString<T>(string conditionColumnName, List<T> tList);

        //string convertToSqlString(string conditionColumnName, FilterObjectDataCustom filterObjectDataCustom);

        string getDbTypeString(IDbTypeCustom dbType);

        Type getValueType(IDbTypeCustom dbType);

        Int32 ExecuteScalar(ITransactionCustom Transaction, ICommandCustom command);

        Int32 ExecuteScalar(IConnectionCustom Connection, ICommandCustom command);

        Int32 ExecuteScalar(string connectionString, ICommandCustom command);

        Int32 ExecuteScalar(ICommandCustom command);

        Int32 ExecuteNonQuery(ITransactionCustom Transaction, ICommandCustom command);

        Int32 ExecuteNonQuery(IConnectionCustom Connection, ICommandCustom command);

        Int32 ExecuteNonQuery(string connectionString, ICommandCustom command);

        Int32 ExecuteNonQuery(ICommandCustom command);

        DataSet ExecuteDataSet(ITransactionCustom Transaction, ICommandCustom command);

        DataSet ExecuteDataSet(IConnectionCustom Connection, ICommandCustom command);

        DataSet ExecuteDataSet(string connectionString, ICommandCustom command);

        DataSet ExecuteDataSet(ICommandCustom command);

        DataTable ExecuteDataTable(ITransactionCustom Transaction, ICommandCustom command);

        DataTable ExecuteDataTable(IConnectionCustom Connection, ICommandCustom command);

        DataTable ExecuteDataTable(string connectionString, ICommandCustom command);

        DataTable ExecuteDataTable(ICommandCustom command);

        Boolean ExecuteNonQuery(ITransactionCustom Transaction, List<ICommandCustom> commandList);

        Boolean ExecuteNonQuery(IConnectionCustom Connection, List<ICommandCustom> commandList);

        Boolean ExecuteNonQuery(string connectionString, List<ICommandCustom> commandList);

        Boolean ExecuteNonQuery(List<ICommandCustom> commandList);
    }
}