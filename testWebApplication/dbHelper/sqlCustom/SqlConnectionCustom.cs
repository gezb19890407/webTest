using System.Data.SqlClient;

namespace System.Data
{
    public class SqlConnectionCustom : IConnectionCustom
    {
        public static string CONNECTION_STRING = ConfigHelper.getValue("connectionString");

        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get { return DatabaseTypeCustom.Sql; }
        }

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
            get
            {
                if (_iDbConnection == null)
                {
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new Exception("数据库连接字符串不存在");
                    }
                    else
                    {
                        _iDbConnection = new SqlConnection(connectionString);
                    }
                }
                return _iDbConnection;
            }
        }
        private IDbConnection _iDbConnection = null;

        public SqlConnectionCustom(IDbConnection iDbConnection)
        {
            _iDbConnection = iDbConnection;
        }

        public SqlConnectionCustom(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnectionCustom()
            : this(string.Empty)
        {
        }
    }
}