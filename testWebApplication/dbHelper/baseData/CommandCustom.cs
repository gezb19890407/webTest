using System;
using System.Data;
//using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
//using MySql.Data.MySqlClient;

namespace System.Data
{
    public class CommandCustom : ICommandCustom
    {
        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get { return _fwDatabaseType; }
        }
        private DatabaseTypeCustom _fwDatabaseType;

        public IDbCommand IDbCommand
        {
            get
            {
                return _ICommandCustom.IDbCommand;
            }
        }

        private ICommandCustom _ICommandCustom;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">影响的记录条数</param>
        /// <param name="_ComparisonType">影响的比较规则</param>
        public CommandCustom(DatabaseTypeCustom databaseTypeCustom, Int64 recordCount, ComparisonTypeCustom comparisonTypeCustom)
        {
            _fwDatabaseType = databaseTypeCustom;
            switch (_fwDatabaseType)
            {
                case DatabaseTypeCustom.Sql:
                    _ICommandCustom = new SqlCommandCustom(recordCount, comparisonTypeCustom);
                    break;
                    //case DatabaseTypeCustom.Oracle:
                    //    //iFWCommand = new FWOracleCommand(recordCount, comparisonTypeCustom);
                    //    break;
                    //case DatabaseTypeCustom.Odbc:
                    //    _ICommandCustom = new FWOdbcCommand(recordCount, comparisonTypeCustom);
                    //    break;
                    //case DatabaseTypeCustom.OleDb:
                    //    _ICommandCustom = new FWOleDbCommand(recordCount, comparisonTypeCustom);
                    //    break;
                    //case DatabaseTypeCustom.MySql:
                    //    _ICommandCustom = new FWMySqlCommand(recordCount, comparisonTypeCustom);
                    //    break;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">必须大于影响的记录条数</param>
        public CommandCustom(DatabaseTypeCustom databaseTypeCustom, Int64 recordCount)
            : this(databaseTypeCustom, recordCount, ComparisonTypeCustom.Than)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">必须大于影响的记录条数</param>
        public CommandCustom(DatabaseTypeCustom databaseTypeCustom)
            : this(databaseTypeCustom, -1)
        {
        }

        public CommandCustom(ITransactionCustom iTransactionCustom)
            : this(iTransactionCustom != null ? iTransactionCustom.DatabaseTypeCustom : (new ConnectionCustom()).DatabaseTypeCustom)
        {
        }

        public CommandCustom(IConnectionCustom connectionCustom)
            : this(connectionCustom != null ? connectionCustom.DatabaseTypeCustom : (new ConnectionCustom()).DatabaseTypeCustom)
        {
        }

        public CommandCustom(string connectionString)
            : this(new ConnectionCustom(connectionString))
        {
        }

        public CommandCustom()
            : this(string.Empty)
        {
        }

        public CommandType CommandType
        {
            set { _ICommandCustom.CommandType = value; }
            get { return _ICommandCustom.CommandType; }
        }

        public string CommandText
        {
            set { _ICommandCustom.CommandText = value; }
            get { return _ICommandCustom.CommandText; }
        }

        public IParameterCollectionCustom Parameters
        {
            set { _ICommandCustom.Parameters = value; }
            get { return _ICommandCustom.Parameters; }
        }

        /// <summary>
        /// 应该影响的记录数
        /// </summary>
        public Int64 recordCount
        {
            get { return _ICommandCustom.recordCount; }
            set { _ICommandCustom.recordCount = value; }
        }

        /// <summary>
        /// 影响的比较规则
        /// </summary>
        public ComparisonTypeCustom ComparisonTypeCustom
        {
            get { return _ICommandCustom.ComparisonTypeCustom; }
            set { _ICommandCustom.ComparisonTypeCustom = value; }
        }

        /// <summary>
        /// 判断是否执行成功
        /// </summary>
        /// <param name="impactRecordCount">影响的记录条数</param>
        /// <returns></returns>
        public Boolean comparison(Int64 impactRecordCount)
        {
            return _ICommandCustom.comparison(impactRecordCount);
        }


    }
}