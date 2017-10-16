using System;
using System.Data;
using System.Data.SqlClient;

namespace System.Data
{
    public class SqlCommandCustom : ICommandCustom
    {
        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get { return DatabaseTypeCustom.Sql; }
        }

        public IDbCommand IDbCommand
        {
            get
            {
                _iDbCommand.CommandType = CommandType;
                _iDbCommand.CommandText = CommandText;
                _iDbCommand.Parameters.Clear();
                SqlCommand SqlCommand = (SqlCommand)_iDbCommand;
                foreach (var Parameter in Parameters)
                {
                    if (Parameter.ParameterType != null)
                    {
                        if (Parameter.ParameterType == typeof(byte[]) || Parameter.ParameterType == typeof(byte?[]))
                        {
                            SqlParameter SqlParameter = new SqlParameter(Parameter.ParameterName, SqlDbType.Image);
                            SqlParameter.Value = Parameter.Value;
                            SqlCommand.Parameters.Add(SqlParameter);
                        }
                        else
                        {
                            SqlCommand.Parameters.AddWithValue(Parameter.ParameterName, Parameter.Value);
                        }
                    }
                    else
                    {
                        SqlCommand.Parameters.AddWithValue(Parameter.ParameterName, Parameter.Value);
                    }
                }
                return _iDbCommand;
            }
        }
        private IDbCommand _iDbCommand;

        /// <summary>
        /// 应该影响的记录数
        /// </summary>
        public Int64 recordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        private Int64 _recordCount;

        /// <summary>
        /// 影响的比较规则
        /// </summary>
        public ComparisonTypeCustom ComparisonTypeCustom
        {
            get { return _ComparisonTypeCustom; }
            set { _ComparisonTypeCustom = value; }
        }
        private ComparisonTypeCustom _ComparisonTypeCustom;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">必须大于影响的记录条数</param>
        public SqlCommandCustom()
            : this(-1)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">必须大于影响的记录条数</param>
        public SqlCommandCustom(Int64 recordCount)
            : this(recordCount, ComparisonTypeCustom.Than)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">必须大于影响的记录条数</param>
        public SqlCommandCustom(string commonText)
            : this(-1, ComparisonTypeCustom.Than)
        {
            CommandText = commonText;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_recordCount">影响的记录条数</param>
        /// <param name="_ComparisonType">影响的比较规则</param>
        public SqlCommandCustom(Int64 recordCount, ComparisonTypeCustom ComparisonTypeCustom)
        {
            _iDbCommand = new SqlCommand();
            _recordCount = recordCount;
            _ComparisonTypeCustom = ComparisonTypeCustom;
        }

        /// <summary>
        /// 判断是否执行成功
        /// </summary>
        /// <param name="impactRecordCount">影响的记录条数</param>
        /// <returns></returns>
        public Boolean comparison(Int64 impactRecordCount)
        {
            Boolean isSuccess = false;
            switch (ComparisonTypeCustom)
            {
                case ComparisonTypeCustom.Than:
                    isSuccess = impactRecordCount > recordCount;
                    break;

                case ComparisonTypeCustom.Less:
                    isSuccess = impactRecordCount < recordCount;
                    break;

                case ComparisonTypeCustom.Equal:
                    isSuccess = impactRecordCount == recordCount;
                    break;

                case ComparisonTypeCustom.ThanEqual:
                    isSuccess = impactRecordCount >= recordCount;
                    break;
            }
            return isSuccess;
        }

        public CommandType CommandType
        {
            set { _CommandType = value; }
            get { return _CommandType; }
        }
        private CommandType _CommandType = CommandType.Text;

        public string CommandText
        {
            set;
            get;
        }

        public IParameterCollectionCustom Parameters
        {
            set { _Parameters = value; }
            get { return _Parameters; }
        }
        private IParameterCollectionCustom _Parameters = new ParameterCollectionCustom();


    }
}