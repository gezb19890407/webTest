using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System;

namespace System.Data
{
    public class CommandCustomHelper : ICommandCustomHelper
    {
        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get
            {
                if (_DatabaseTypeCustom == DatabaseTypeCustom.Inherit)
                {
                    _DatabaseTypeCustom = ConnectionCustom.getDatabaseType(connectionString);
                }
                return _DatabaseTypeCustom;
            }
        }
        private DatabaseTypeCustom _DatabaseTypeCustom = DatabaseTypeCustom.Inherit;

        public string connectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = connectionString;
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        private string _connectionString;

        private ICommandCustomHelper _iFWCommandHelper;

        public CommandCustomHelper(DatabaseTypeCustom DatabaseTypeCustom)
        {
            _DatabaseTypeCustom = DatabaseTypeCustom;
            switch (_DatabaseTypeCustom)
            {
                case DatabaseTypeCustom.Sql:
                    _iFWCommandHelper = new SqlCommandHelper();
                    break;
                    //case DatabaseTypeCustom.Oracle:
                    //    //ICommandCustomHelper = new FWOracleCommandHelper();
                    //    break;
                    //case DatabaseTypeCustom.Odbc:
                    //    _iFWCommandHelper = new FWOdbcCommandHelper();
                    //    break;
                    //case DatabaseTypeCustom.OleDb:
                    //    _iFWCommandHelper = new FWOleDbCommandHelper();
                    //    break;
                    //case DatabaseTypeCustom.MySql:
                    //    _iFWCommandHelper = new FWMySqlCommandHelper();
                    //    break;
            }
        }

        public CommandCustomHelper(ITransactionCustom iTransactionCustom)
            : this(iTransactionCustom.DatabaseTypeCustom)
        {
        }

        public CommandCustomHelper(IConnectionCustom iConnectionCustom)
            : this(iConnectionCustom.DatabaseTypeCustom)
        {
            _connectionString = iConnectionCustom.connectionString;
        }

        public CommandCustomHelper(string connectionString)
            : this((new ConnectionCustom(connectionString)))
        {
        }

        public CommandCustomHelper()
            : this(string.Empty)
        {
        }

        public bool isCanConnection(string connectionString)
        {
            return _iFWCommandHelper.isCanConnection(connectionString);
        }

        public bool isCanConnection()
        {
            return _iFWCommandHelper.isCanConnection();
        }

        public string checkParam(string param)
        {
            return _iFWCommandHelper.checkParam(param);
        }

        public string joinToSqlString<T>(List<T> tList)
        {
            return _iFWCommandHelper.joinToSqlString<T>(tList);
        }

        public string convertToSqlString<T>(string conditionColumnName, List<T> tList)
        {
            throw new NotImplementedException();
        }

        public string convertToSqlString(string conditionColumnName, FilterObjectDataCustom filterObjectDataCustom)
        {
            throw new NotImplementedException();
        }

        public string getDbTypeString(IDbTypeCustom dbType)
        {
            return _iFWCommandHelper.getDbTypeString(dbType);
        }

        public Type getValueType(IDbTypeCustom dbType)
        {
            return _iFWCommandHelper.getValueType(dbType);
        }

        public int ExecuteScalar(ITransactionCustom Transaction, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteScalar(Transaction, Command);
        }

        public int ExecuteScalar(IConnectionCustom Connection, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteScalar(Connection, Command);
        }

        public int ExecuteScalar(string connectionString, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteScalar(connectionString, Command);
        }

        public int ExecuteScalar(ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteScalar(connectionString, Command);
        }

        public int ExecuteNonQuery(ITransactionCustom Transaction, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteNonQuery(Transaction, Command);
        }

        public int ExecuteNonQuery(IConnectionCustom Connection, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteNonQuery(Connection, Command);
        }

        public int ExecuteNonQuery(string connectionString, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteNonQuery(connectionString, Command);
        }

        public int ExecuteNonQuery(ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteNonQuery(connectionString, Command);
        }

        public DataSet ExecuteDataSet(ITransactionCustom Transaction, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataSet(Transaction, Command);
        }

        public DataSet ExecuteDataSet(IConnectionCustom Connection, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataSet(Connection, Command);
        }

        public DataSet ExecuteDataSet(string connectionString, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataSet(connectionString, Command);
        }

        public DataSet ExecuteDataSet(ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataSet(connectionString, Command);
        }

        public DataTable ExecuteDataTable(ITransactionCustom Transaction, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataTable(Transaction, Command);
        }

        public DataTable ExecuteDataTable(IConnectionCustom Connection, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataTable(Connection, Command);
        }

        public DataTable ExecuteDataTable(string connectionString, ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataTable(connectionString, Command);
        }

        public DataTable ExecuteDataTable(ICommandCustom Command)
        {
            return _iFWCommandHelper.ExecuteDataTable(connectionString, Command);
        }

        public bool ExecuteNonQuery(ITransactionCustom Transaction, List<ICommandCustom> CommandList)
        {
            return _iFWCommandHelper.ExecuteNonQuery(Transaction, CommandList);
        }

        public bool ExecuteNonQuery(IConnectionCustom Connection, List<ICommandCustom> CommandList)
        {
            return _iFWCommandHelper.ExecuteNonQuery(Connection, CommandList);
        }

        public bool ExecuteNonQuery(string connectionString, List<ICommandCustom> CommandList)
        {
            return _iFWCommandHelper.ExecuteNonQuery(connectionString, CommandList);
        }

        public bool ExecuteNonQuery(List<ICommandCustom> CommandList)
        {
            return _iFWCommandHelper.ExecuteNonQuery(connectionString, CommandList);
        }
    }
}