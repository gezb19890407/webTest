using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace System.Data
{
    public class SqlCommandStaticHelper
    {
        public static bool isCanConnection(string connectionString)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.isCanConnection(connectionString);
        }

        public static bool isCanConnection()
        {
            return isCanConnection(null);
        }

        public static string checkParam(string param)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.checkParam(param);
        }

        public static string joinToSqlString<T>(List<T> tList)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.joinToSqlString(tList);
        }

        //public static string convertToSqlString<T>(string conditionColumnName, List<T> tList)
        //{
        //    SqlCommandHelper commandHelper = new SqlCommandHelper();
        //    return commandHelper.convertToSqlString(conditionColumnName, tList);
        //}

        //public static string convertToSqlString(string conditionColumnName, FWFilterObjectData fwFilterObjectData)
        //{
        //    SqlCommandHelper commandHelper = new SqlCommandHelper();
        //    return commandHelper.convertToSqlString(conditionColumnName, fwFilterObjectData);
        //}

        public static string getDbTypeString(IDbTypeCustom dbType)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.getDbTypeString(dbType);
        }

        public static int ExecuteScalar(string connectionString, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteScalar(connectionString, Command);
        }

        public static int ExecuteScalar(ICommandCustom Command)
        {
            return ExecuteScalar(string.Empty, Command);
        }

        public static int ExecuteScalar(IConnectionCustom Connection, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteScalar(Connection, Command);
        }

        public static int ExecuteScalar(ITransactionCustom Transaction, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteScalar(Transaction, Command);
        }

        public static int ExecuteNonQuery(string connectionString, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(connectionString, Command);
        }

        public static int ExecuteNonQuery(ICommandCustom Command)
        {
            return ExecuteNonQuery(string.Empty, Command);
        }

        public static int ExecuteNonQuery(IConnectionCustom Connection, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(Connection, Command);
        }

        public static int ExecuteNonQuery(ITransactionCustom Transaction, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(Transaction, Command);
        }

        public static DataSet ExecuteDataSet(string connectionString, IDbCommand Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(connectionString, Command);
        }

        public static DataSet ExecuteDataSet(IDbCommand Command)
        {
            return ExecuteDataSet(string.Empty, Command);
        }

        public static DataSet ExecuteDataSet(string connectionString, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(connectionString, Command);
        }

        public static DataSet ExecuteDataSet(ICommandCustom Command)
        {
            return ExecuteDataSet(string.Empty, Command);
        }

        public static DataSet ExecuteDataSet(IDbConnection Connection, IDbCommand Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(Connection, Command);
        }

        public static DataSet ExecuteDataSet(IConnectionCustom Connection, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(Connection, Command);
        }

        public static DataSet ExecuteDataSet(IDbTransaction Transaction, IDbCommand Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(Transaction, Command);
        }

        public static DataSet ExecuteDataSet(ITransactionCustom Transaction, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataSet(Transaction, Command);
        }

        public static DataTable ExecuteDataTable(string connectionString, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataTable(connectionString, Command);
        }

        public static DataTable ExecuteDataTable(string connectionString, string commandText)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            SqlCommandCustom command = new SqlCommandCustom();
            command.CommandText = commandText;
            return commandHelper.ExecuteDataTable(connectionString, command);
        }
        public static DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(string.Empty, commandText);
        }
        public static DataTable ExecuteDataTable(ICommandCustom Command)
        {
            return ExecuteDataTable(string.Empty, Command);
        }

        public static DataTable ExecuteDataTable(IConnectionCustom Connection, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataTable(Connection, Command);
        }

        public static DataTable ExecuteDataTable(ITransactionCustom Transaction, ICommandCustom Command)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteDataTable(Transaction, Command);
        }

        public static bool ExecuteNonQuery(string connectionString, List<ICommandCustom> CommandList)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(connectionString, CommandList);
        }

        public static bool ExecuteNonQuery(List<ICommandCustom> CommandList)
        {
            return ExecuteNonQuery(string.Empty, CommandList);
        }

        public static bool ExecuteNonQuery(IConnectionCustom Connection, List<ICommandCustom> CommandList)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(Connection, CommandList);
        }

        public static bool ExecuteNonQuery(ITransactionCustom Transaction, List<ICommandCustom> CommandList)
        {
            SqlCommandHelper commandHelper = new SqlCommandHelper();
            return commandHelper.ExecuteNonQuery(Transaction, CommandList);
        }


    }
}