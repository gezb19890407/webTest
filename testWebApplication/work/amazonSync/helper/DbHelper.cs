using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data.Common;
using System.Configuration;
using System.Diagnostics;

namespace testWebApplication.work.amazonSync.helper
{
    public abstract class DbHelper
    {

        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings["db_titlekeyword"].ConnectionString;

        public static void SetTimeoutDefault()
        {
            Timeout = 30;
        }
        public static int Timeout = 30;

        public static IDbBase Provider = new DbBase();

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteNonQuery(sqlConnectionString, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection conn = Provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteReader(sqlConnectionString, cmdType, cmdText, commandParameters);
        }
        public static DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText.ToLower(), commandParameters);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">连接字符串
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DbDataReader ExecuteReaderPage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string GroupClause, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
                DbCommand cmd = Provider.CreateCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, "", commandParameters);
                string Sql = GetPageSql(conn, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount);
                if (GroupClause != null && GroupClause.Trim() != "")
                {
                    int n = Sql.ToLower().LastIndexOf(" order by ");
                    Sql = Sql.Substring(0, n) + " " + GroupClause + " " + Sql.Substring(n);
                }
                cmd.CommandText = Sql.ToLower();
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public static DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteScalar(sqlConnectionString, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            cmdText = cmdText.ToLower();
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            return ExecuteTable(sqlConnectionString, cmdType, cmdText, commandParameters);
        }

        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {

                // cmdText = cmdText.ToLower();

                connection.ConnectionString = connectionString;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                DbDataAdapter ap = Provider.CreateDataAdapter();
                ap.SelectCommand = cmd;
                DataSet st = new DataSet();
                ap.Fill(st, "Result");
                cmd.Parameters.Clear();
                return st.Tables["Result"];
            }
        }

        public static DataTable ExecuteTable(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "Result");
            cmd.Parameters.Clear();
            return st.Tables["Result"];
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            return ExecutePage(sqlConnectionString, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">SQL数据库连接字符串</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = Provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                return ExecutePage(connection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
            }
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connection">SQL数据库连接对象</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(DbConnection connection, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, CommandType.Text, "", commandParameters);
            string Sql = GetPageSql(connection, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount);
            cmd.CommandText = Sql;
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "PageResult");
            cmd.Parameters.Clear();
            return st.Tables["PageResult"];
        }
        /// <summary>
        /// 取得分页的SQL语句
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <param name="SqlAllFields"></param>
        /// <param name="SqlTablesAndWhere"></param>
        /// <param name="IndexField"></param>
        /// <param name="OrderFields"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        private static string GetPageSql(DbConnection connection, DbCommand cmd, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            //RecordCount = 0;
            //PageCount = 0;
            //if (PageSize <= 0)
            //{
            //    PageSize = 10;
            //}
            //string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            //cmd.CommandText = SqlCount.ToLower();

            //try
            //{
            //    RecordCount = Convert.ToInt32(cmd.ExecuteScalar());   // (int)cmd.ExecuteScalar();
            //}
            //catch (Exception ex)
            //{ 

            //}
            //if (RecordCount % PageSize == 0)
            //{
            //    PageCount = RecordCount / PageSize;
            //}
            //else
            //{
            //    PageCount = RecordCount / PageSize + 1;
            //}
            //if (PageIndex > PageCount)
            //    PageIndex = PageCount;
            //if (PageIndex < 1)
            //    PageIndex = 1;
            //string Sql = null;
            //if (PageIndex == 1)
            //{
            //    Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            //}
            //else
            //{
            //    Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
            //    if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
            //    {
            //        string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //        Sql += _where + ") and (";
            //    }
            //    else
            //    {
            //        Sql += SqlTablesAndWhere + " where (";
            //    }
            //    Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
            //    Sql += ")) " + OrderFields;
            //}
            //return Sql;


            RecordCount = 0;
            PageCount = 0;
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            cmd.CommandText = SqlCount.ToLower();

            try
            {
                RecordCount = Convert.ToInt32(cmd.ExecuteScalar());   // (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {

            }
            if (RecordCount % PageSize == 0)
            {
                PageCount = RecordCount / PageSize;
            }
            else
            {
                PageCount = RecordCount / PageSize + 1;
            }
            if (PageIndex > PageCount)
                PageIndex = PageCount;
            if (PageIndex < 1)
                PageIndex = 1;
            string Sql = null;
            if (PageIndex == 1)
            {
                Sql = "select  " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            }
            else
            {
                Sql = "select  " + SqlAllFields + " from ";
                if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += SqlTablesAndWhere + " where (";
                }
                // Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields + "limit" + (PageIndex - 1) * PageSize + " " + IndexField;
                Sql += IndexField + " not in (  select " + IndexField + " from  (   select  " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields + " limit " + (PageIndex - 1) * PageSize + ") as a ";


                Sql += ")) " + OrderFields;

            }
            Sql += " limit  " + PageSize;
            return Sql;

        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;// cmdText.ToLower();

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                    if (parm != null)
                        cmd.Parameters.Add(parm);
            }
        }

        #region SqlBulk 
        public static void Insert<T>(string tableName, List<T> entityList)
        {
            //Stopwatch sw = new Stopwatch();
            DataTable dt = GetTableSchemaByName(sqlConnectionString, tableName);

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = dt.Rows.Count;
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                for (int i = 0; i < entityList.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        dr[propertyInfo.Name] = propertyInfo.GetValue(entityList[i]);
                    }
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        public static void Update<T>(string tableName, List<T> entityList)
        {
            //Stopwatch sw = new Stopwatch();
            DataTable dt = GetTableSchemaByName(sqlConnectionString, tableName);

            using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = dt.Rows.Count;
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                for (int i = 0; i < entityList.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        dr[propertyInfo.Name] = propertyInfo.GetValue(entityList[i]);
                    }
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        static DataTable GetTableSchemaByName(string sqlConnectionString, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
SELECT TOP 1 * FROM {0}", tableName);
            DataTable dt = ExecuteTable(CommandType.Text, sb.ToString(), null);
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Clear();
            }
            return dt;
        }

        static int GetMinute(long l)
        {
            return (Int32)l / 60000;
        }
        #endregion

        public static void ValiduteDataTable<T>(List<T> entityList, ref List<T> successList, ref List<T> errorList)
        {
            ValiduteDataTable(entityList, ref successList, ref errorList, typeof(T).Name);
        }

        public static void ValiduteDataTable<T>(List<T> entityList, ref List<T> refSuccessList, ref List<T> refErrorList, string tableNane)
        {
            if (entityList == null || entityList.Count == 0)
            {
                return;
            }
            List<T> successList = new List<T>();
            List<T> errorList = new List<T>();
            DataTable tableColumns = GetTableColumnsByName(sqlConnectionString, tableNane);
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            List<string> errorStringList;
            entityList.ForEach((entity) =>
            {
                if (valudateEntity(entity, sqlConnectionString, tableColumns, properties, out errorStringList))
                {
                    successList.Add(entity);
                }
                else
                {
                    errorList.Add(entity);
                }
            });
            refSuccessList = successList;
            refErrorList = errorList;
        }

        public static bool valudateEntity<T>(T entity, string sqlConnectionString, out List<string> errorList)
        {
            return valudateEntity<T>(entity, sqlConnectionString, null, null, out errorList);
        }

        public static bool valudateEntity<T>(T entity, DataTable tableColumns, PropertyInfo[] properties, out List<string> errorList)
        {
            return valudateEntity<T>(entity, null, tableColumns, properties, out errorList);
        }

        public static bool valudateEntity<T>(T entity, string sqlConnectionString, DataTable tableColumns, PropertyInfo[] properties, out List<string> errorList)
        {
            errorList = new List<string>();
            tableColumns = tableColumns == null ? GetTableColumnsByName(sqlConnectionString, typeof(T).Name) : tableColumns;
            properties = properties == null ? typeof(T).GetProperties() : properties;
            bool isPass = true;
            int length = 0, rowLength = 0;
            int decimalDigit = 0, rowDecimalDigit = 0;
            object value;
            string columnName;

            foreach (PropertyInfo propertyInfo in properties)
            {
                isPass = false;
                columnName = propertyInfo.Name;

                DataRow row = tableColumns.Select("columnName = '" + columnName + "'").First();
                //验证：1、判空 2、长度
                if (row != null)
                {
                    isPass = true;
                    value = propertyInfo.GetValue(entity);

                    if (value == null)
                    {
                        if (row["isnullable"].ToString() == "0")
                        {
                            isPass = false;
                            errorList.Add(columnName + "不能为空！");
                        }
                    }
                    else
                    {
                        rowLength = int.Parse(row["length"].ToString());
                        rowDecimalDigit = int.Parse(row["decimalDigit"].ToString());
                        if (propertyInfo.PropertyType == typeof(string) && rowLength < value.ToString().Length)
                        {
                            isPass = false;
                            errorList.Add(columnName + "长度超过设定值！");
                        }
                        if (propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?)
                            || propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?)
                            || propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?))
                        {
                            length = value.ToString().IndexOf(".") == -1 ? value.ToString().Length : value.ToString().IndexOf(".");
                            decimalDigit = value.ToString().Length - length - 1;

                            if (decimalDigit < 0 && rowLength < length)
                            {
                                isPass = false;
                                errorList.Add(columnName + "长度超过设定值！");
                            }
                            else if (decimalDigit > 0)
                            {
                                if (rowLength < length || rowDecimalDigit < decimalDigit)
                                {
                                    isPass = false;
                                    errorList.Add(columnName + "小数位长度超过设定值！");
                                }
                            }
                        }
                    }
                }
            }
            return isPass;
        }

        public static PropertyInfo[] GetPropertyInfos<T>()
        {
            return typeof(T).GetProperties();
        }

        public static DataTable GetTableColumnsByName(string sqlConnectionString, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
--快速查看表结构  
SELECT  col.colorder AS  serialNumber,--序号
        col.name AS columnName ,--列名
        ISNULL(ep.[value], '') AS columnDesc ,--列说明
        t.name AS dateType ,--数据类型
        col.length AS length ,--长度
        ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS DecimalDigit ,--小数位数
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN CAST(1 AS bit)
             ELSE CAST(0 AS bit)
        END AS IsIdentity ,--标识
        CASE WHEN EXISTS ( SELECT   1
                           FROM     dbo.sysindexes si
                                    INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                                                              AND si.indid = sik.indid
                                    INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                                                              AND sc.colid = sik.colid
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name
                                                              AND so.xtype = 'PK'
                           WHERE    sc.id = col.id
                                    AND sc.colid = col.colid ) THEN CAST(1 AS bit)
             ELSE CAST(0 AS bit)
        END AS IsPrimary ,--主键
        CASE WHEN col.isnullable = 1 THEN CAST(1 AS bit)
             ELSE CAST(0 AS bit)
        END AS isnullable,--允许空 
        ISNULL(comm.text, '') AS defaultValue-- 默认值
FROM    dbo.syscolumns col
        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
        INNER JOIN dbo.sysobjects obj ON col.id = obj.id
                                         AND obj.xtype = 'U'
                                         AND obj.status >= 0
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                 AND col.colid = ep.minor_id
                                                 AND ep.name = 'MS_Description'
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                    AND epTwo.minor_id = 0
                                                    AND epTwo.name = 'MS_Description'
WHERE   obj.name = '{0}'--表名  ", tableName);
            return ExecuteTable(CommandType.Text, sb.ToString(), null);
        }
    }
}
