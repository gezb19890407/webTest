using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace System.Data
{
    internal class ConnectionCustom : IConnectionCustom
    {
        public static string CONNECTION_STRING = ConfigHelper.getValue("connectionString");

        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get
            {
                if (_fwDatabaseType == DatabaseTypeCustom.Inherit)
                {
                    _fwDatabaseType = ConnectionCustom.getDatabaseType(connectionString);
                }
                return _fwDatabaseType;
            }
        }
        private DatabaseTypeCustom _fwDatabaseType = DatabaseTypeCustom.Inherit;

        public string connectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = CONNECTION_STRING;
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        private string _connectionString;

        public IDbConnection iDbConnection
        {
            get { return _ConnectionCustom.iDbConnection; }
        }

        private IConnectionCustom _ConnectionCustom = null;

        public ConnectionCustom(string connectionString)
        {
            _connectionString = connectionString;
            switch (DatabaseTypeCustom)
            {
                case DatabaseTypeCustom.Sql:
                    _ConnectionCustom = new SqlConnectionCustom(connectionString);
                    break;
                    //case DatabaseTypeCustom.Oracle:
                    //    _ConnectionCustom = new FWMySqlConnection(connectionString);
                    //    break;
                    //case DatabaseTypeCustom.Odbc:
                    //    _ConnectionCustom = new FWOdbcConnection(connectionString);
                    //    break;
                    //case DatabaseTypeCustom.OleDb:
                    //    _ConnectionCustom = new FWOleDbConnection(connectionString);
                    //    break;
                    //case DatabaseTypeCustom.MySql:
                    //    _ConnectionCustom = new FWMySqlConnection(connectionString);
                    //    break;
            }
        }

        public ConnectionCustom()
            : this(string.Empty)
        {
        }


        /// <summary>
        /// 所有数据库字符串对应的数据库类型
        /// </summary>
        private static Dictionary<string, DatabaseTypeCustom> fwDatabaseTypeDictionary = new Dictionary<string, DatabaseTypeCustom>();
        public static DatabaseTypeCustom getDatabaseType(string connectionString)
        {
            DatabaseTypeCustom DatabaseTypeCustom;
            if (fwDatabaseTypeDictionary.ContainsKey(connectionString))
            {
                DatabaseTypeCustom = fwDatabaseTypeDictionary[connectionString];
            }
            else
            {
                if (connectionString.ToLower().IndexOf("Data Source=".ToLower()) > -1 && connectionString.ToLower().IndexOf("Database=".ToLower()) > -1 && connectionString.ToLower().IndexOf("User ID=".ToLower()) > -1 && connectionString.ToLower().IndexOf("Password=".ToLower()) > -1)
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.Sql;
                }
                else if ((new Regex("Oracle")).IsMatch(connectionString))
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.Oracle;
                }
                else if ((new Regex("Sql")).IsMatch(connectionString))
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.Odbc;
                }
                else if (connectionString.ToLower().IndexOf("Microsoft.Jet.Oledb".ToLower()) > -1 && connectionString.ToLower().IndexOf("Data Source=".ToLower()) > -1)
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.OleDb;
                }
                else if (connectionString.ToLower().IndexOf("Microsoft.ACE.OLEDB.12.0".ToLower()) > -1)
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.OleDb;
                }
                else if (connectionString.ToLower().IndexOf("Server=".ToLower()) > -1 && connectionString.ToLower().IndexOf("Database=".ToLower()) > -1 && connectionString.ToLower().IndexOf("Uid=".ToLower()) > -1 && connectionString.ToLower().IndexOf("Pwd=".ToLower()) > -1)
                {
                    DatabaseTypeCustom = DatabaseTypeCustom.MySql;
                }
                else
                {
                    throw new Exception("连接字符串 未能分析出数据库类型");
                }
                fwDatabaseTypeDictionary[connectionString] = DatabaseTypeCustom;
            }
            return DatabaseTypeCustom;
        }

    }
}