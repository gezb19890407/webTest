namespace System.Data
{
    public class SqlTransactionCustom : ITransactionCustom
    {
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
                    _connectionString = SqlConnectionCustom.CONNECTION_STRING;
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        private string _connectionString;

        private IDbConnection _iDbConnection = null;

        public IDbTransaction IDbTransaction
        {
            get { return _iDbTransaction; }
        }
        private IDbTransaction _iDbTransaction = null;

        public SqlTransactionCustom(IDbConnection iDbConnection)
        {
            _iDbConnection = iDbConnection;
        }

        public SqlTransactionCustom(string connectionString)
            : this((new SqlConnectionCustom(connectionString)).iDbConnection)
        {
        }

        public SqlTransactionCustom()
            : this(string.Empty)
        {
        }

        public void Open()
        {
            _iDbConnection.Open();
        }

        public void BeginTransaction()
        {
            if (_iDbConnection != null)
            {
                _iDbConnection.Open();
                _iDbTransaction = _iDbConnection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_iDbTransaction != null)
            {
                _iDbTransaction.Commit();
                _iDbConnection.Close();
                _iDbTransaction = null;
            }
        }

        public void Rollback()
        {
            if (_iDbTransaction != null)
            {
                _iDbTransaction.Rollback();
                _iDbConnection.Close();
                _iDbTransaction = null;
            }
        }

        public void Close()
        {
            _iDbConnection.Close();
        }
    }
}