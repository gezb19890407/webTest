using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace System.Data
{
    public class DbTypeHelper
    {
        public static string getDbName(DatabaseTypeCustom fwDatabaseType)
        {
            string dbName = null;
            switch (fwDatabaseType)
            {
                case DatabaseTypeCustom.Sql:
                    dbName = "SqlDb";
                    break;
                case DatabaseTypeCustom.Oracle:
                    dbName = "OracleDb";
                    break;
                case DatabaseTypeCustom.Odbc:
                    dbName = "OdbcDb";
                    break;
                case DatabaseTypeCustom.OleDb:
                    dbName = "OleDb";
                    break;
                case DatabaseTypeCustom.MySql:
                    dbName = "MySqlDb";
                    break;
            }
            return dbName;
        }

        public static string getDbName(Type dbType)
        {
            string dbName = null;
            //if (dbType == typeof(DbTypeCustom))
            //{
            //    dbName = "Db";
            //}
            //else 
            if (dbType == typeof(SqlDbTypeCustom))
            {
                dbName = "SqlDb";
            }
            //else if (dbType == typeof(FWOracleDbType))
            //{
            //    dbName = "OracleDb";
            //}
            //else if (dbType == typeof(FWOdbcDbType))
            //{
            //    dbName = "OdbcDb";
            //}
            //else if (dbType == typeof(FWOleDbType))
            //{
            //    dbName = "OleDb";
            //}
            //else if (dbType == typeof(FWMySqlDbType))
            //{
            //    dbName = "MySqlDb";
            //}
            return dbName;
        }

        public static string getDbName(IDbTypeCustom iDbTypeCustom)
        {
            return getDbName(iDbTypeCustom.GetType());
        }

        public static Int32 getDbTypeCode(IDbTypeCustom iDbTypeCustom)
        {
            Int32 dbTypeCode = -1;
            Type dbType = iDbTypeCustom.GetType();
            //if (dbType == typeof(SqlDbTypeCustom))
            //{
            //    dbTypeCode = Convert.ToInt32(((SqlDbTypeCustom)iDbTypeCustom).dbType);
            //}
            //else if (dbType == typeof(FWOracleDbType))
            //{
            //    dbTypeCode = Convert.ToInt32(((FWOracleDbType)iDbTypeCustom).dbType);
            //}
            //else if (dbType == typeof(FWOdbcDbType))
            //{
            //    dbTypeCode = Convert.ToInt32(((FWOdbcDbType)iDbTypeCustom).dbType);
            //}
            //else if (dbType == typeof(FWOleDbType))
            //{
            //    dbTypeCode = Convert.ToInt32(((FWOleDbType)iDbTypeCustom).dbType);
            //}
            //else if (dbType == typeof(FWMySqlDbType))
            //{
            //    dbTypeCode = Convert.ToInt32(((FWMySqlDbType)iDbTypeCustom).dbType);
            //}
            PropertyInfo pi = dbType.GetProperty("dbType");
            dbTypeCode = Convert.ToInt32(pi.GetValue(iDbTypeCustom, null));
            return dbTypeCode;
        }


        public static SqlDbTypeCustom getSqlDBType(DbTypeCustom fwDBType)
        {
            SqlDbTypeCustom fwSqlDBType = new SqlDbTypeCustom();
            fwSqlDBType.length = fwDBType.length;
            fwSqlDBType.precison = fwDBType.precison;
            fwSqlDBType.scale = fwDBType.scale;
            switch (fwDBType.dbType)
            {
                case DbType.AnsiString:
                    // 摘要:
                    //     非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。       
                    fwSqlDBType.dbType = SqlDbType.VarChar;
                    break;
                case DbType.Binary:
                    //
                    // 摘要:
                    //     二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Byte:
                    //
                    // 摘要:
                    //     一个 8 位无符号整数，范围在 0 到 255 之间。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Boolean:
                    //
                    // 摘要:
                    //     简单类型，表示 true 或 false 的布尔值。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Currency:
                    //
                    // 摘要:
                    //     货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Date:
                    //
                    // 摘要:
                    //     表示日期值的类型。       
                    fwSqlDBType.dbType = SqlDbType.Date;
                    break;
                case DbType.DateTime:
                    //
                    // 摘要:
                    //     表示一个日期和时间值的类型。       
                    fwSqlDBType.dbType = SqlDbType.DateTime;
                    break;
                case DbType.Decimal:
                    //
                    // 摘要:
                    //     简单类型，表示从 1.0 x 10 -28 到大约 7.9 x 10 28 且有效位数为 28 到 29 位的值。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Double:
                    //
                    // 摘要:
                    //     浮点型，表示从大约 5.0 x 10 -324 到 1.7 x 10 308 且精度为 15 到 16 位的值。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Guid:
                    //
                    // 摘要:
                    //     全局唯一标识符（或 GUID）。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Int16:
                    //
                    // 摘要:
                    //     整型，表示值介于 -32768 到 32767 之间的有符号 16 位整数。       
                    fwSqlDBType.dbType = SqlDbType.SmallInt;
                    break;
                case DbType.Int32:
                    //
                    // 摘要:
                    //     整型，表示值介于 -2147483648 到 2147483647 之间的有符号 32 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Int64:
                    //
                    // 摘要:
                    //     整型，表示值介于 -9223372036854775808 到 9223372036854775807 之间的有符号 64 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Object:
                    //
                    // 摘要:
                    //     常规类型，表示任何没有由其他 DbType 值显式表示的引用或值类型。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.SByte:
                    //
                    // 摘要:
                    //     整型，表示值介于 -128 到 127 之间的有符号 8 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Single:
                    //
                    // 摘要:
                    //     浮点型，表示从大约 1.5 x 10 -45 到 3.4 x 10 38 且精度为 7 位的值。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.String:
                    //
                    // 摘要:
                    //     表示 Unicode 字符串的类型。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Time:
                    //
                    // 摘要:
                    //     表示时间值的类型。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.UInt16:
                    //
                    // 摘要:
                    //     整型，表示值介于 0 到 65535 之间的无符号 16 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.UInt32:
                    //
                    // 摘要:
                    //     整型，表示值介于 0 到 4294967295 之间的无符号 32 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.UInt64:
                    //
                    // 摘要:
                    //     整型，表示值介于 0 到 18446744073709551615 之间的无符号 64 位整数。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.VarNumeric:
                    //
                    // 摘要:
                    //     变长数值。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.AnsiStringFixedLength:
                    //
                    // 摘要:
                    //     非 Unicode 字符的固定长度流。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.StringFixedLength:
                    //
                    // 摘要:
                    //     Unicode 字符的定长串。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.Xml:
                    //
                    // 摘要:
                    //     XML 文档或片段的分析表示。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.DateTime2:
                    //
                    // 摘要:
                    //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
                    //     100 毫微秒。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                case DbType.DateTimeOffset:
                    //
                    // 摘要:
                    //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
                    //     100 毫微秒。时区值范围从 -14:00 到 +14:00。       
                    //fwSqlDBType.dbType = SqlDbType.;
                    break;
                default:
                    throw new Exception("SqlDbType中不存在OleDbType中值为" + fwDBType.dbType + "的映射对象");
                    break;

            }
            return fwSqlDBType;
        }


        public static DbTypeCustom getDBType(SqlDbTypeCustom fwSqlDBType)
        {
            DbTypeCustom fwDBType = new DbTypeCustom();
            fwDBType.length = fwSqlDBType.length;
            fwDBType.precison = fwSqlDBType.precison;
            fwDBType.scale = fwSqlDBType.scale;
            switch (fwSqlDBType.dbType)
            {
                case SqlDbType.BigInt:
                    // 摘要:
                    //     System.Int64。64 位的有符号整数。
                    fwDBType.dbType = DbType.Int64;
                    break;
                case SqlDbType.Binary:
                    //
                    // 摘要:
                    //     System.Byte 类型的 System.Array。二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。
                    fwDBType.dbType = DbType.Binary;
                    break;
                case SqlDbType.Bit:
                    //
                    // 摘要:
                    //     System.Boolean。无符号数值，可以是 0、1 或 null。
                    fwDBType.dbType = DbType.Boolean;
                    break;
                case SqlDbType.Char:
                    //
                    // 摘要:
                    //     System.String。非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。
                    fwDBType.dbType = DbType.AnsiStringFixedLength;
                    break;
                case SqlDbType.DateTime:
                    //
                    // 摘要:
                    //     System.DateTime。日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。
                    fwDBType.dbType = DbType.DateTime;
                    break;
                case SqlDbType.Decimal:
                    //
                    // 摘要:
                    //     System.Decimal。固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。
                    fwDBType.dbType = DbType.Decimal;
                    break;
                case SqlDbType.Float:
                    //
                    // 摘要:
                    //     System.Double。-1.79E +308 到 1.79E +308 范围内的浮点数。
                    fwDBType.dbType = DbType.Double;
                    break;
                case SqlDbType.Image:
                    //
                    // 摘要:
                    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。
                    fwDBType.dbType = DbType.Binary;
                    break;
                case SqlDbType.Int:
                    //
                    // 摘要:
                    //     System.Int32。32 位的有符号整数。
                    fwDBType.dbType = DbType.Int32;
                    break;
                case SqlDbType.Money:
                    //
                    // 摘要:
                    //     System.Decimal。货币值，范围在 -2 63（即 -9,223,372,036,854,775,808）到 2 63 -1（即 +9,223,372,036,854,775,807）之间，精度为千分之十个货币单位。
                    fwDBType.dbType = DbType.Currency;
                    break;
                case SqlDbType.NChar:
                    //
                    // 摘要:
                    //     System.String。Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。
                    fwDBType.dbType = DbType.StringFixedLength;
                    break;
                case SqlDbType.NText:
                    //
                    // 摘要:
                    //     System.String。Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。
                    fwDBType.dbType = DbType.String;
                    break;
                case SqlDbType.NVarChar:
                    //
                    // 摘要:
                    //     System.String。Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。如果字符串大于 4,000 个字符，隐式转换会失败。在使用比
                    //     4,000 个字符更长的字符串时，请显式设置对象。
                    fwDBType.dbType = DbType.String;
                    break;
                case SqlDbType.Real:
                    //
                    // 摘要:
                    //     System.Single。-3.40E +38 到 3.40E +38 范围内的浮点数。
                    fwDBType.dbType = DbType.Single;
                    break;
                case SqlDbType.UniqueIdentifier:
                    //
                    // 摘要:
                    //     System.Guid。全局唯一标识符（或 GUID）。
                    fwDBType.dbType = DbType.Guid;
                    break;
                case SqlDbType.SmallDateTime:
                    //
                    // 摘要:
                    //     System.DateTime。日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。
                    fwDBType.dbType = DbType.DateTime;
                    break;
                case SqlDbType.SmallInt:
                    //
                    // 摘要:
                    //     System.Int16。16 位的有符号整数。
                    fwDBType.dbType = DbType.Int16;
                    break;
                case SqlDbType.SmallMoney:
                    //
                    // 摘要:
                    //     System.Decimal。货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。
                    fwDBType.dbType = DbType.Currency;
                    break;
                case SqlDbType.Text:
                    //
                    // 摘要:
                    //     System.String。非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。
                    fwDBType.dbType = DbType.AnsiString;
                    break;
                case SqlDbType.Timestamp:
                    //
                    // 摘要:
                    //     System.Byte 类型的 System.Array。自动生成的二进制数，并保证其在数据库中唯一。timestamp 通常用作对表中各行的版本进行标记的机制。存储大小为
                    //     8 字节。
                    fwDBType.dbType = DbType.Binary;
                    break;
                case SqlDbType.TinyInt:
                    //
                    // 摘要:
                    //     System.Byte。8 位的无符号整数。
                    fwDBType.dbType = DbType.Byte;
                    break;
                case SqlDbType.VarBinary:
                    //
                    // 摘要:
                    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。如果字节数组大于 8,000
                    //     个字节，隐式转换会失败。在使用比 8,000 个字节大的字节数组时，请显式设置对象。
                    fwDBType.dbType = DbType.Binary;
                    break;
                case SqlDbType.VarChar:
                    //
                    // 摘要:
                    //     System.String。非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
                    fwDBType.dbType = DbType.AnsiString;
                    break;
                case SqlDbType.Variant:
                    //
                    // 摘要:
                    //     System.Object。特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。
                    fwDBType.dbType = DbType.Object;
                    break;
                case SqlDbType.Xml:
                    //
                    // 摘要:
                    //     XML 值。使用 System.Data.SqlClient.SqlDataReader.GetValue(System.Int32) 方法或 System.Data.SqlTypes.SqlXml.Value
                    //     属性获取字符串形式的 XML，或通过调用 System.Data.SqlTypes.SqlXml.CreateReader() 方法获取 System.Xml.XmlReader
                    //     形式的 XML。
                    fwDBType.dbType = DbType.Xml;
                    break;
                case SqlDbType.Udt:
                    //
                    // 摘要:
                    //     SQL Server 2005 用户定义的类型 (UDT)。
                    fwDBType.dbType = DbType.Object;
                    break;
                case SqlDbType.Structured:
                    //
                    // 摘要:
                    //     指定表值参数中包含的构造数据的特殊数据类型。
                    fwDBType.dbType = DbType.Object;
                    break;
                case SqlDbType.Date:
                    //
                    // 摘要:
                    //     日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。
                    fwDBType.dbType = DbType.Date;
                    break;
                case SqlDbType.Time:
                    //
                    // 摘要:
                    //     基于 24 小时制的时间数据。时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。
                    fwDBType.dbType = DbType.DateTime;
                    break;
                case SqlDbType.DateTime2:
                    //
                    // 摘要:
                    //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
                    //     100 毫微秒。
                    fwDBType.dbType = DbType.DateTime2;
                    break;
                case SqlDbType.DateTimeOffset:
                    //
                    // 摘要:
                    //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
                    //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
                    fwDBType.dbType = DbType.DateTimeOffset;
                    break;
            }
            return fwDBType;
        }

        #region 停用

        /// <summary>
        /// dbType //数据库类型 DbType、SqlDbType
        /// dbDataType //数据库SQL中查出的字段在数据库中的类型
        /// </summary>
        /// <param name="fwDatabaseType"></param>
        /// <param name="dbColumnTypeCode"></param>
        /// <returns></returns>
        //public static string getDataDbTypeCode(DatabaseTypeCustom fwDatabaseType, string dbColumnTypeCode)
        //{
        //    string dbName = DbTypeHelper.getDbName(fwDatabaseType);
        //    string dataDbTypeCode = null;
        //    FWOleDbCommand fwOleDbCommand = new FWOleDbCommand();
        //    fwOleDbCommand.CommandText = string.Format(@"
        //select top 1 {0}TypeCode as dbTypeCode
        //from [TypeMapping$]
        //where 
        //{0}ColumnTypeCode=@dbColumnTypeCode", dbName);
        //    //fwOleDbCommand.CommandText = "select * from [TypeMapping$]";
        //    fwOleDbCommand.Parameters.AddWithValue("@dbColumnTypeCode", dbColumnTypeCode);
        //    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "db.xls") + "\";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
        //    DataTable dt = FWOleDbCommandStaticHelper.ExecuteDataTable(connectionString, fwOleDbCommand);

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        dataDbTypeCode = dt.Rows[0]["dbTypeCode"].ToString();
        //    }

        //    return dataDbTypeCode;
        //}

        //public static T getDbType<T>(IDbTypeCustom iDbTypeCustom)
        //{
        //    Type fromDbType = iDbTypeCustom.GetType();
        //    Type toDbType = typeof(T);
        //    if (fromDbType == toDbType)
        //    {
        //        return (T)iDbTypeCustom;
        //    }
        //    T result = default(T);
        //    result = (T)Activator.CreateInstance(toDbType, true);
        //    string fromDbName = DbTypeHelper.getDbName(fromDbType);
        //    string toDbName = DbTypeHelper.getDbName(toDbType);
        //    PropertyInfo pi = fromDbType.GetProperty("dbType");
        //    Int32 fromDbTypeCode = Convert.ToInt32(pi.GetValue(iDbTypeCustom, null));
        //    FWOleDbCommand fwOleDbCommand = new FWOleDbCommand();
        //    fwOleDbCommand.CommandText = string.Format(@"
        //select top 1 {0}TypeCode as typeCode
        //from [TypeMapping$]
        //where 
        //{1}TypeCode=@fromDbTypeCode", toDbName, fromDbName);
        //    //fwOleDbCommand.CommandText = "select * from [TypeMapping$]";
        //    fwOleDbCommand.Parameters.AddWithValue("@fromDbTypeCode", fromDbTypeCode);
        //    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "db.xls") + "\";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
        //    DataTable dt = FWOleDbCommandStaticHelper.ExecuteDataTable(connectionString, fwOleDbCommand);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        pi = toDbType.GetProperty("dbType");
        //        pi.SetValue(result, Enum.Parse(pi.PropertyType, dt.Rows[0]["typeCode"].ToString()), null);
        //    }
        //    pi = toDbType.GetProperty("length");
        //    pi.SetValue(result, fromDbType.GetProperty("length").GetValue(iDbTypeCustom, null), null);
        //    pi = toDbType.GetProperty("precison");
        //    pi.SetValue(result, fromDbType.GetProperty("precison").GetValue(iDbTypeCustom, null), null);
        //    pi = toDbType.GetProperty("scale");
        //    pi.SetValue(result, fromDbType.GetProperty("scale").GetValue(iDbTypeCustom, null), null);
        //    return result;
        //}

        //public static DataTypeCustom getFWDataType(IDbTypeCustom iDbTypeCustom)
        //{
        //    DataTypeCustom fwDataType = DataTypeCustom.String;
        //    string dbName = getDbName(iDbTypeCustom);
        //    Int32 dbTypeCode = getDbTypeCode(iDbTypeCustom);
        //    FWOleDbCommand fwOleDbCommand = new FWOleDbCommand();
        //    fwOleDbCommand.CommandText = string.Format(@"
        //select top 1 TypeCode
        //from [TypeMapping$]
        //where 
        //{0}TypeCode=@dbTypeCode", dbName);
        //    //fwOleDbCommand.CommandText = "select * from [TypeMapping$]";
        //    fwOleDbCommand.Parameters.AddWithValue("@dbTypeCode", dbTypeCode);
        //    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "db.xls") + "\";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
        //    DataTable dt = FWOleDbCommandStaticHelper.ExecuteDataTable(connectionString, fwOleDbCommand);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        fwDataType = (DataTypeCustom)(Convert.ToInt32(dt.Rows[0]["TypeCode"]));
        //    }
        //    return fwDataType;
        //}

        //public static DictionaryCustom<DataTypeCustom, Type> fwDataTypeSystemTypeDictionary = null;
        //public static Type getType(IDbTypeCustom iDbTypeCustom)
        //{
        //    if (fwDataTypeSystemTypeDictionary == null)
        //    {
        //        fwDataTypeSystemTypeDictionary = new DictionaryCustom<DataTypeCustom, Type>();
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Boolean] = typeof(bool);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Byte] = typeof(byte);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.ByteArray] = typeof(byte[]);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.UInt16] = typeof(UInt16);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Short] = typeof(short);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.UInt32] = typeof(UInt32);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Int] = typeof(int);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.UInt64] = typeof(UInt64);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Long] = typeof(long);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Float] = typeof(float);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Decimal] = typeof(decimal);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Double] = typeof(double);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Char] = typeof(char);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.String] = typeof(string);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.DateTime] = typeof(DateTime);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Guid] = typeof(Guid);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.DateTimeOffset] = typeof(DateTimeOffset);
        //        fwDataTypeSystemTypeDictionary[DataTypeCustom.Object] = typeof(object);
        //    }
        //    return fwDataTypeSystemTypeDictionary[getFWDataType(iDbTypeCustom)];
        //}


        //public static FWOleDbType getOleDbDBType(DbTypeCustom fwDBType)
        //{
        //    FWOleDbType fwOleDbDBType = new FWOleDbType();
        //    fwOleDbDBType.length = fwDBType.length;
        //    fwOleDbDBType.precison = fwDBType.precison;
        //    fwOleDbDBType.scale = fwDBType.scale;
        //    switch (fwDBType.dbType)
        //    {
        //        case DbType.AnsiString:
        //            // 摘要:
        //            //     非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。       
        //            fwOleDbDBType.dbType = OleDbType.VarChar;
        //            break;
        //        case DbType.Binary:
        //            //
        //            // 摘要:
        //            //     二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。       
        //            fwOleDbDBType.dbType = OleDbType.Binary;
        //            break;
        //        case DbType.Byte:
        //            //
        //            // 摘要:
        //            //     一个 8 位无符号整数，范围在 0 到 255 之间。       
        //            fwOleDbDBType.dbType = OleDbType.UnsignedTinyInt;
        //            break;
        //        case DbType.Boolean:
        //            //
        //            // 摘要:
        //            //     简单类型，表示 true 或 false 的布尔值。       
        //            fwOleDbDBType.dbType = OleDbType.Boolean;
        //            break;
        //        case DbType.Currency:
        //            //
        //            // 摘要:
        //            //     货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。       
        //            fwOleDbDBType.dbType = OleDbType.Currency;
        //            break;
        //        case DbType.Date:
        //            //
        //            // 摘要:
        //            //     表示日期值的类型。       
        //            fwOleDbDBType.dbType = OleDbType.Date;
        //            break;
        //        case DbType.DateTime:
        //            //
        //            // 摘要:
        //            //     表示一个日期和时间值的类型。       
        //            fwOleDbDBType.dbType = OleDbType.Date;
        //            break;
        //        case DbType.Decimal:
        //            //
        //            // 摘要:
        //            //     简单类型，表示从 1.0 x 10 -28 到大约 7.9 x 10 28 且有效位数为 28 到 29 位的值。       
        //            fwOleDbDBType.dbType = OleDbType.Decimal;
        //            break;
        //        case DbType.Double:
        //            //
        //            // 摘要:
        //            //     浮点型，表示从大约 5.0 x 10 -324 到 1.7 x 10 308 且精度为 15 到 16 位的值。       
        //            fwOleDbDBType.dbType = OleDbType.Double;
        //            break;
        //        case DbType.Guid:
        //            //
        //            // 摘要:
        //            //     全局唯一标识符（或 GUID）。       
        //            fwOleDbDBType.dbType = OleDbType.Guid;
        //            break;
        //        case DbType.Int16:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -32768 到 32767 之间的有符号 16 位整数。       
        //            fwOleDbDBType.dbType = OleDbType.SmallInt;
        //            break;
        //        case DbType.Int32:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -2147483648 到 2147483647 之间的有符号 32 位整数。 
        //            fwOleDbDBType.dbType = OleDbType.Integer;
        //            break;
        //        case DbType.Int64:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -9223372036854775808 到 9223372036854775807 之间的有符号 64 位整数。       
        //            fwOleDbDBType.dbType = OleDbType.BigInt;
        //            break;
        //        case DbType.Object:
        //            //
        //            // 摘要:
        //            //     常规类型，表示任何没有由其他 DbType 值显式表示的引用或值类型。       
        //            fwOleDbDBType.dbType = OleDbType.IUnknown;
        //            break;
        //        case DbType.SByte:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -128 到 127 之间的有符号 8 位整数。      TinyInt 
        //            fwOleDbDBType.dbType = OleDbType.TinyInt;
        //            break;
        //        case DbType.Single:
        //            //
        //            // 摘要:
        //            //     浮点型，表示从大约 1.5 x 10 -45 到 3.4 x 10 38 且精度为 7 位的值。       
        //            fwOleDbDBType.dbType = OleDbType.Single;
        //            break;
        //        case DbType.String:
        //            //
        //            // 摘要:
        //            //     表示 Unicode 字符串的类型。       
        //            fwOleDbDBType.dbType = OleDbType.BSTR;
        //            break;
        //        case DbType.Time:
        //            //
        //            // 摘要:
        //            //     表示时间值的类型。        
        //            fwOleDbDBType.dbType = OleDbType.DBTime;
        //            break;
        //        case DbType.UInt16:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 65535 之间的无符号 16 位整数。    
        //            fwOleDbDBType.dbType = OleDbType.UnsignedSmallInt;
        //            break;
        //        case DbType.UInt32:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 4294967295 之间的无符号 32 位整数。       
        //            fwOleDbDBType.dbType = OleDbType.UnsignedInt;
        //            break;
        //        case DbType.UInt64:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 18446744073709551615 之间的无符号 64 位整数。       
        //            fwOleDbDBType.dbType = OleDbType.UnsignedBigInt;
        //            break;
        //        case DbType.VarNumeric:
        //            //
        //            // 摘要:
        //            //     变长数值。       
        //            fwOleDbDBType.dbType = OleDbType.VarNumeric;
        //            break;
        //        case DbType.AnsiStringFixedLength:
        //            //
        //            // 摘要:
        //            //     非 Unicode 字符的固定长度流。       
        //            fwOleDbDBType.dbType = OleDbType.Char;
        //            break;
        //        case DbType.StringFixedLength:
        //            //
        //            // 摘要:
        //            //     Unicode 字符的定长串。       
        //            fwOleDbDBType.dbType = OleDbType.WChar;
        //            break;
        //        case DbType.Xml:
        //            //
        //            // 摘要:
        //            //     XML 文档或片段的分析表示。       
        //            //fwOleDbDBType.dbType = OleDbType.WChar;
        //            break;
        //        case DbType.DateTime2:
        //            //
        //            // 摘要:
        //            //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。       
        //            //fwOleDbDBType.dbType = OleDbType.WChar;
        //            break;
        //        case DbType.DateTimeOffset:
        //            //
        //            // 摘要:
        //            //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。时区值范围从 -14:00 到 +14:00。       
        //            //fwOleDbDBType.dbType = OleDbType.WChar;
        //            break;
        //        default:
        //            throw new Exception("SqlDbType中不存在OleDbType中值为" + fwDBType.dbType + "的映射对象");
        //            break;

        //    }
        //    return fwOleDbDBType;
        //}
        //public static FWMySqlDbType getMySqlDBType(DbTypeCustom fwDBType)
        //{
        //    FWMySqlDbType fwMySqlDBType = new FWMySqlDbType();
        //    fwMySqlDBType.length = fwDBType.length;
        //    fwMySqlDBType.precison = fwDBType.precison;
        //    fwMySqlDBType.scale = fwDBType.scale;
        //    switch (fwDBType.dbType)
        //    {
        //        case DbType.AnsiString:
        //            // 摘要:
        //            //     非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。       
        //            fwMySqlDBType.dbType = MySqlDbType.VarChar;
        //            break;
        //        case DbType.Binary:
        //            //
        //            // 摘要:
        //            //     二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。       
        //            fwMySqlDBType.dbType = MySqlDbType.Binary;
        //            break;
        //        case DbType.Byte:
        //            //
        //            // 摘要:
        //            //     一个 8 位无符号整数，范围在 0 到 255 之间。       
        //            fwMySqlDBType.dbType = MySqlDbType.Byte;
        //            break;
        //        case DbType.Boolean:
        //            //
        //            // 摘要:
        //            //     简单类型，表示 true 或 false 的布尔值。       
        //            fwMySqlDBType.dbType = MySqlDbType.Bit;
        //            break;
        //        case DbType.Currency:
        //            //
        //            // 摘要:
        //            //     货币值，范围在 -2 63（即 -922,337,203,685,477.5808）到 2 63 -1（即 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.Date:
        //            //
        //            // 摘要:
        //            //     表示日期值的类型。       
        //            fwMySqlDBType.dbType = MySqlDbType.Date;
        //            break;
        //        case DbType.DateTime:
        //            //
        //            // 摘要:
        //            //     表示一个日期和时间值的类型。       
        //            fwMySqlDBType.dbType = MySqlDbType.DateTime;
        //            break;
        //        case DbType.Decimal:
        //            //
        //            // 摘要:
        //            //     简单类型，表示从 1.0 x 10 -28 到大约 7.9 x 10 28 且有效位数为 28 到 29 位的值。       
        //            fwMySqlDBType.dbType = MySqlDbType.Decimal;
        //            break;
        //        case DbType.Double:
        //            //
        //            // 摘要:
        //            //     浮点型，表示从大约 5.0 x 10 -324 到 1.7 x 10 308 且精度为 15 到 16 位的值。       
        //            fwMySqlDBType.dbType = MySqlDbType.Double;
        //            break;
        //        case DbType.Guid:
        //            //
        //            // 摘要:
        //            //     全局唯一标识符（或 GUID）。       
        //            //fwMySqlDBType.dbType = MySqlDbType.Guid;
        //            break;
        //        case DbType.Int16:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -32768 到 32767 之间的有符号 16 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.Int16;
        //            break;
        //        case DbType.Int32:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -2147483648 到 2147483647 之间的有符号 32 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.Int32;
        //            break;
        //        case DbType.Int64:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -9223372036854775808 到 9223372036854775807 之间的有符号 64 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.Int64;
        //            break;
        //        case DbType.Object:
        //            //
        //            // 摘要:
        //            //     常规类型，表示任何没有由其他 DbType 值显式表示的引用或值类型。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.SByte:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 -128 到 127 之间的有符号 8 位整数。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.Single:
        //            //
        //            // 摘要:
        //            //     浮点型，表示从大约 1.5 x 10 -45 到 3.4 x 10 38 且精度为 7 位的值。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.String:
        //            //
        //            // 摘要:
        //            //     表示 Unicode 字符串的类型。       
        //            fwMySqlDBType.dbType = MySqlDbType.String;
        //            break;
        //        case DbType.Time:
        //            //
        //            // 摘要:
        //            //     表示时间值的类型。       
        //            fwMySqlDBType.dbType = MySqlDbType.Time;
        //            break;
        //        case DbType.UInt16:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 65535 之间的无符号 16 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.UInt16;
        //            break;
        //        case DbType.UInt32:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 4294967295 之间的无符号 32 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.UInt32;
        //            break;
        //        case DbType.UInt64:
        //            //
        //            // 摘要:
        //            //     整型，表示值介于 0 到 18446744073709551615 之间的无符号 64 位整数。       
        //            fwMySqlDBType.dbType = MySqlDbType.UInt64;
        //            break;
        //        case DbType.VarNumeric:
        //            //
        //            // 摘要:
        //            //     变长数值。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.AnsiStringFixedLength:
        //            //
        //            // 摘要:
        //            //     非 Unicode 字符的固定长度流。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.StringFixedLength:
        //            //
        //            // 摘要:
        //            //     Unicode 字符的定长串。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.Xml:
        //            //
        //            // 摘要:
        //            //     XML 文档或片段的分析表示。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.DateTime2:
        //            //
        //            // 摘要:
        //            //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        case DbType.DateTimeOffset:
        //            //
        //            // 摘要:
        //            //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。时区值范围从 -14:00 到 +14:00。       
        //            //fwMySqlDBType.dbType = MySqlDbType.;
        //            break;
        //        default:
        //            throw new Exception("SqlDbType中不存在OleDbType中值为" + fwDBType.dbType + "的映射对象");
        //            break;

        //    }
        //    return fwMySqlDBType;
        //}

        //public static DbTypeCustom getDBType(FWOleDbType fwOleDbDBType)
        //{
        //    DbTypeCustom fwDBType = new DbTypeCustom();
        //    fwDBType.length = fwOleDbDBType.length;
        //    fwDBType.precison = fwOleDbDBType.precison;
        //    fwDBType.scale = fwOleDbDBType.scale;
        //    switch (fwOleDbDBType.dbType)
        //    {
        //        case OleDbType.Empty:
        //            // 摘要:
        //            //     无任何值 (DBTYPE_EMPTY)。
        //            fwDBType.dbType = DbType.Object;
        //            break;
        //        case OleDbType.SmallInt:
        //            //
        //            // 摘要:
        //            //     16 位带符号的整数 (DBTYPE_I2)。它映射到 System.Int16。
        //            fwDBType.dbType = DbType.Int16;
        //            break;
        //        case OleDbType.Integer:
        //            //
        //            // 摘要:
        //            //     32 位带符号的整数 (DBTYPE_I4)。它映射到 System.Int32。
        //            fwDBType.dbType = DbType.Int32;
        //            break;
        //        case OleDbType.Single:
        //            //
        //            // 摘要:
        //            //     浮点数字，范围在 -3.40E +38 到 3.40E +38 之间 (DBTYPE_R4)。它映射到 System.Single。
        //            fwDBType.dbType = DbType.Single;
        //            break;
        //        case OleDbType.Double:
        //            //
        //            // 摘要:
        //            //     浮点数字，范围在 -1.79E +308 到 1.79E +308 之间 (DBTYPE_R8)。它映射到 System.Double。
        //            fwDBType.dbType = DbType.Double;
        //            break;
        //        case OleDbType.Currency:
        //            //
        //            // 摘要:
        //            //     一个货币值，范围在 -2 63（或 -922,337,203,685,477.5808）到 2 63 -1（或 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位
        //            //     (DBTYPE_CY)。它映射到 System.Decimal。
        //            fwDBType.dbType = DbType.Currency;
        //            break;
        //        case OleDbType.Date:
        //            //
        //            // 摘要:
        //            //     日期数据，存储为双精度型 (DBTYPE_DATE)。整数部分是自 1899 年 12 月 30 日以来的天数，而小数部分是不足一天的部分。它映射到
        //            //     System.DateTime。
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case OleDbType.BSTR:
        //            //
        //            // 摘要:
        //            //     以 null 终止的 Unicode 字符串 (DBTYPE_BSTR)。它映射到 System.String。
        //            fwDBType.dbType = DbType.String;
        //            break;
        //        case OleDbType.IDispatch:
        //            //
        //            // 摘要:
        //            //     指向 IDispatch 接口的指针 (DBTYPE_IDISPATCH)。它映射到 System.Object。
        //            fwDBType.dbType = DbType.Object;
        //            break;
        //        case OleDbType.Error:
        //            //
        //            // 摘要:
        //            //     32 位错误代码 (DBTYPE_ERROR)。它映射到 System.Exception。
        //            fwDBType.dbType = DbType.Int32;
        //            break;
        //        case OleDbType.Boolean:
        //            //
        //            // 摘要:
        //            //     布尔值 (DBTYPE_BOOL)。它映射到 System.Boolean。
        //            fwDBType.dbType = DbType.Boolean;
        //            break;
        //        case OleDbType.Variant:
        //            //
        //            // 摘要:
        //            //     可包含数字、字符串、二进制或日期数据以及特殊值 Empty 和 Null 的特殊数据类型 (DBTYPE_VARIANT)。如果未指定任何其他类型，则假定为该类型。它映射到
        //            //     System.Object。
        //            fwDBType.dbType = DbType.Object;
        //            break;
        //        case OleDbType.IUnknown:
        //            //
        //            // 摘要:
        //            //     指向 IUnknown 接口的指针 (DBTYPE_UNKNOWN)。它映射到 System.Object。
        //            fwDBType.dbType = DbType.Object;
        //            break;
        //        case OleDbType.Decimal:
        //            //
        //            // 摘要:
        //            //     定点精度和小数位数数值，范围在 -10 38 -1 和 10 38 -1 之间 (DBTYPE_DECIMAL)。它映射到 System.Decimal。
        //            fwDBType.dbType = DbType.Decimal;
        //            break;
        //        case OleDbType.TinyInt:
        //            //
        //            // 摘要:
        //            //     8 位带符号的整数 (DBTYPE_I1)。它映射到 System.SByte。
        //            fwDBType.dbType = DbType.SByte;
        //            break;
        //        case OleDbType.UnsignedTinyInt:
        //            //
        //            // 摘要:
        //            //     8 位无符号整数 (DBTYPE_UI1)。它映射到 System.Byte。
        //            fwDBType.dbType = DbType.Byte;
        //            break;
        //        case OleDbType.UnsignedSmallInt:
        //            //
        //            // 摘要:
        //            //     16 位无符号整数 (DBTYPE_UI2)。它映射到 System.UInt16。
        //            fwDBType.dbType = DbType.UInt16;
        //            break;
        //        case OleDbType.UnsignedInt:
        //            //
        //            // 摘要:
        //            //     32 位无符号整数 (DBTYPE_UI4)。它映射到 System.UInt32。
        //            fwDBType.dbType = DbType.UInt32;
        //            break;
        //        case OleDbType.BigInt:
        //            //
        //            // 摘要:
        //            //     64 位带符号的整数 (DBTYPE_I8)。它映射到 System.Int64。
        //            fwDBType.dbType = DbType.Int64;
        //            break;
        //        case OleDbType.UnsignedBigInt:
        //            //
        //            // 摘要:
        //            //     64 位无符号整数 (DBTYPE_UI8)。它映射到 System.UInt64。
        //            fwDBType.dbType = DbType.UInt64;
        //            break;
        //        case OleDbType.Filetime:
        //            //
        //            // 摘要:
        //            //     64 位无符号整数，表示自 1601 年 1 月 1 日以来 100 个纳秒间隔的数字 (DBTYPE_FILETIME)。它映射到 System.DateTime。
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case OleDbType.Guid:
        //            //
        //            // 摘要:
        //            //     全局唯一标识符（或 GUID） (DBTYPE_GUID)。它映射到 System.Guid。
        //            fwDBType.dbType = DbType.Guid;
        //            break;
        //        case OleDbType.Binary:
        //            //
        //            // 摘要:
        //            //     二进制数据流 (DBTYPE_BYTES)。它映射到 System.Byte 类型的 System.Array。
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case OleDbType.Char:
        //            //
        //            // 摘要:
        //            //     字符串 (DBTYPE_STR)。它映射到 System.String。
        //            fwDBType.dbType = DbType.AnsiStringFixedLength;
        //            break;
        //        case OleDbType.WChar:
        //            //
        //            // 摘要:
        //            //     以 null 终止的 Unicode 字符流 (DBTYPE_WSTR)。它映射到 System.String。
        //            fwDBType.dbType = DbType.StringFixedLength;
        //            break;
        //        case OleDbType.Numeric:
        //            //
        //            // 摘要:
        //            //     具有定点精度和小数位数的精确数值 (DBTYPE_NUMERIC)。它映射到 System.Decimal。
        //            fwDBType.dbType = DbType.Decimal;
        //            break;
        //        case OleDbType.DBDate:
        //            //
        //            // 摘要:
        //            //     格式为 yyyymmdd 的日期数据 (DBTYPE_DBDATE)。它映射到 System.DateTime。
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case OleDbType.DBTime:
        //            //
        //            // 摘要:
        //            //     格式为 hhmmss 的时间数据 (DBTYPE_DBTIME)。它映射到 System.TimeSpan。
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case OleDbType.DBTimeStamp:
        //            //
        //            // 摘要:
        //            //     格式为 yyyymmddhhmmss 的日期和时间数据 (DBTYPE_DBTIMESTAMP)。它映射到 System.DateTime。
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case OleDbType.PropVariant:
        //            //
        //            // 摘要:
        //            //     自动化 PROPVARIANT (DBTYPE_PROP_VARIANT)。它映射到 System.Object。
        //            fwDBType.dbType = DbType.Object;
        //            break;
        //        case OleDbType.VarNumeric:
        //            //
        //            // 摘要:
        //            //     变长数值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Decimal。
        //            fwDBType.dbType = DbType.VarNumeric;
        //            break;
        //        case OleDbType.VarChar:
        //            //
        //            // 摘要:
        //            //     非 Unicode 字符的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwDBType.dbType = DbType.AnsiString;
        //            break;
        //        case OleDbType.LongVarChar:
        //            //
        //            // 摘要:
        //            //     长的字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwDBType.dbType = DbType.AnsiString;
        //            break;
        //        case OleDbType.VarWChar:
        //            //
        //            // 摘要:
        //            //     长可变、以 null 终止的 Unicode 字符流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwDBType.dbType = DbType.String;
        //            break;
        //        case OleDbType.LongVarWChar:
        //            //
        //            // 摘要:
        //            //     长的以 null 终止的 Unicode 字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwDBType.dbType = DbType.String;
        //            break;
        //        case OleDbType.VarBinary:
        //            //
        //            // 摘要:
        //            //     二进制数据的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case OleDbType.LongVarBinary:
        //            //
        //            // 摘要:
        //            //     长的二进制值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        default:
        //            throw new Exception("SqlDbType中不存在OleDbType中值为" + fwOleDbDBType.dbType + "的映射对象");
        //            break;

        //    }
        //    return fwDBType;
        //}
        //public static DbTypeCustom getDBType(FWMySqlDbType fwMySqlDBType)
        //{
        //    DbTypeCustom fwDBType = new DbTypeCustom();
        //    fwDBType.length = fwMySqlDBType.length;
        //    fwDBType.precison = fwMySqlDBType.precison;
        //    fwDBType.scale = fwMySqlDBType.scale;
        //    switch (fwMySqlDBType.dbType)
        //    {
        //        case MySqlDbType.Binary:
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case MySqlDbType.Bit:
        //            fwDBType.dbType = DbType.Boolean;
        //            break;
        //        case MySqlDbType.Blob:
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case MySqlDbType.Byte:
        //            fwDBType.dbType = DbType.Byte;
        //            break;
        //        case MySqlDbType.Date:
        //            fwDBType.dbType = DbType.Date;
        //            break;
        //        case MySqlDbType.DateTime:
        //            fwDBType.dbType = DbType.DateTime;
        //            break;
        //        case MySqlDbType.Decimal:
        //            fwDBType.dbType = DbType.Decimal;
        //            break;
        //        case MySqlDbType.Double:
        //            fwDBType.dbType = DbType.Double;
        //            break;
        //        case MySqlDbType.Enum:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Float:
        //            fwDBType.dbType = DbType.Double;
        //            break;
        //        case MySqlDbType.Geometry:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Int16:
        //            fwDBType.dbType = DbType.Int16;
        //            break;
        //        case MySqlDbType.Int24:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Int32:
        //            fwDBType.dbType = DbType.Int32;
        //            break;
        //        case MySqlDbType.Int64:
        //            fwDBType.dbType = DbType.Int64;
        //            break;
        //        case MySqlDbType.LongBlob:
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case MySqlDbType.LongText:
        //            fwDBType.dbType = DbType.AnsiString;
        //            break;
        //        case MySqlDbType.MediumBlob:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.MediumText:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Newdate:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.NewDecimal:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Set:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.String:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Text:
        //            fwDBType.dbType = DbType.String;
        //            break;
        //        case MySqlDbType.Time:
        //            fwDBType.dbType = DbType.Time;
        //            break;
        //        case MySqlDbType.Timestamp:
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case MySqlDbType.TinyBlob:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.TinyText:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.UByte:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.UInt16:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.UInt24:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.UInt32:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.UInt64:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.VarBinary:
        //            fwDBType.dbType = DbType.Binary;
        //            break;
        //        case MySqlDbType.VarChar:
        //            fwDBType.dbType = DbType.AnsiString;
        //            break;
        //        case MySqlDbType.VarString:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //        case MySqlDbType.Year:
        //            //fwDBType.dbType = DbType.;
        //            break;
        //    }
        //    return fwDBType;
        //}


        //public static SqlDbTypeCustom getSqlDBType(FWOleDbType fwOleDbDBType)
        //{
        //    SqlDbTypeCustom fwSqlDBType = new SqlDbTypeCustom();
        //    fwSqlDBType.length = fwOleDbDBType.length;
        //    fwSqlDBType.precison = fwOleDbDBType.precison;
        //    fwSqlDBType.scale = fwOleDbDBType.scale;
        //    switch (fwOleDbDBType.dbType)
        //    {
        //        case OleDbType.Empty:
        //            // 摘要:
        //            //     无任何值 (DBTYPE_EMPTY)。
        //            throw new Exception("SqlDbType中不存在OleDbType.Empty的映射对象");
        //            break;
        //        case OleDbType.SmallInt:
        //            //
        //            // 摘要:
        //            //     16 位带符号的整数 (DBTYPE_I2)。它映射到 System.Int16。
        //            fwSqlDBType.dbType = SqlDbType.SmallInt;
        //            break;
        //        case OleDbType.Integer:
        //            //
        //            // 摘要:
        //            //     32 位带符号的整数 (DBTYPE_I4)。它映射到 System.Int32。
        //            fwSqlDBType.dbType = SqlDbType.Int;
        //            break;
        //        case OleDbType.Single:
        //            //
        //            // 摘要:
        //            //     浮点数字，范围在 -3.40E +38 到 3.40E +38 之间 (DBTYPE_R4)。它映射到 System.Single。
        //            fwSqlDBType.dbType = SqlDbType.Real;
        //            break;
        //        case OleDbType.Double:
        //            //
        //            // 摘要:
        //            //     浮点数字，范围在 -1.79E +308 到 1.79E +308 之间 (DBTYPE_R8)。它映射到 System.Double。
        //            fwSqlDBType.dbType = SqlDbType.Float;
        //            break;
        //        case OleDbType.Currency:
        //            //
        //            // 摘要:
        //            //     一个货币值，范围在 -2 63（或 -922,337,203,685,477.5808）到 2 63 -1（或 +922,337,203,685,477.5807）之间，精度为千分之十个货币单位
        //            //     (DBTYPE_CY)。它映射到 System.Decimal。
        //            fwSqlDBType.dbType = SqlDbType.Money;
        //            break;
        //        case OleDbType.Date:
        //            //
        //            // 摘要:
        //            //     日期数据，存储为双精度型 (DBTYPE_DATE)。整数部分是自 1899 年 12 月 30 日以来的天数，而小数部分是不足一天的部分。它映射到
        //            //     System.DateTime。
        //            fwSqlDBType.dbType = SqlDbType.DateTime;
        //            break;
        //        case OleDbType.BSTR:
        //            //
        //            // 摘要:
        //            //     以 null 终止的 Unicode 字符串 (DBTYPE_BSTR)。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.NText;
        //            break;
        //        case OleDbType.IDispatch:
        //            //
        //            // 摘要:
        //            //     指向 IDispatch 接口的指针 (DBTYPE_IDISPATCH)。它映射到 System.Object。
        //            fwSqlDBType.dbType = SqlDbType.Variant;
        //            break;
        //        case OleDbType.Error:
        //            //
        //            // 摘要:
        //            //     32 位错误代码 (DBTYPE_ERROR)。它映射到 System.Exception。
        //            throw new Exception("SqlDbType中不存在OleDbType.Error的映射对象");
        //            break;
        //        case OleDbType.Boolean:
        //            //
        //            // 摘要:
        //            //     布尔值 (DBTYPE_BOOL)。它映射到 System.Boolean。
        //            fwSqlDBType.dbType = SqlDbType.Bit;
        //            break;
        //        case OleDbType.Variant:
        //            //
        //            // 摘要:
        //            //     可包含数字、字符串、二进制或日期数据以及特殊值 Empty 和 Null 的特殊数据类型 (DBTYPE_VARIANT)。如果未指定任何其他类型，则假定为该类型。它映射到
        //            //     System.Object。
        //            fwSqlDBType.dbType = SqlDbType.Variant;
        //            break;
        //        case OleDbType.IUnknown:
        //            //
        //            // 摘要:
        //            //     指向 IUnknown 接口的指针 (DBTYPE_UNKNOWN)。它映射到 System.Object。
        //            fwSqlDBType.dbType = SqlDbType.Variant;
        //            break;
        //        case OleDbType.Decimal:
        //            //
        //            // 摘要:
        //            //     定点精度和小数位数数值，范围在 -10 38 -1 和 10 38 -1 之间 (DBTYPE_DECIMAL)。它映射到 System.Decimal。
        //            fwSqlDBType.dbType = SqlDbType.Decimal;
        //            break;
        //        case OleDbType.TinyInt:
        //            //
        //            // 摘要:
        //            //     8 位带符号的整数 (DBTYPE_I1)。它映射到 System.SByte。
        //            fwSqlDBType.dbType = SqlDbType.TinyInt;
        //            break;
        //        case OleDbType.UnsignedTinyInt:
        //            //
        //            // 摘要:
        //            //     8 位无符号整数 (DBTYPE_UI1)。它映射到 System.Byte。
        //            fwSqlDBType.dbType = SqlDbType.TinyInt;
        //            break;
        //        case OleDbType.UnsignedSmallInt:
        //            //
        //            // 摘要:
        //            //     16 位无符号整数 (DBTYPE_UI2)。它映射到 System.UInt16。
        //            fwSqlDBType.dbType = SqlDbType.TinyInt;
        //            break;
        //        case OleDbType.UnsignedInt:
        //            //
        //            // 摘要:
        //            //     32 位无符号整数 (DBTYPE_UI4)。它映射到 System.UInt32。
        //            fwSqlDBType.dbType = SqlDbType.Int;
        //            break;
        //        case OleDbType.BigInt:
        //            //
        //            // 摘要:
        //            //     64 位带符号的整数 (DBTYPE_I8)。它映射到 System.Int64。
        //            fwSqlDBType.dbType = SqlDbType.BigInt;
        //            break;
        //        case OleDbType.UnsignedBigInt:
        //            //
        //            // 摘要:
        //            //     64 位无符号整数 (DBTYPE_UI8)。它映射到 System.UInt64。
        //            fwSqlDBType.dbType = SqlDbType.BigInt;
        //            break;
        //        case OleDbType.Filetime:
        //            //
        //            // 摘要:
        //            //     64 位无符号整数，表示自 1601 年 1 月 1 日以来 100 个纳秒间隔的数字 (DBTYPE_FILETIME)。它映射到 System.DateTime。
        //            fwSqlDBType.dbType = SqlDbType.DateTime;
        //            break;
        //        case OleDbType.Guid:
        //            //
        //            // 摘要:
        //            //     全局唯一标识符（或 GUID） (DBTYPE_GUID)。它映射到 System.Guid。
        //            fwSqlDBType.dbType = SqlDbType.UniqueIdentifier;
        //            break;
        //        case OleDbType.Binary:
        //            //
        //            // 摘要:
        //            //     二进制数据流 (DBTYPE_BYTES)。它映射到 System.Byte 类型的 System.Array。
        //            if (fwSqlDBType.length > 0 && fwSqlDBType.length < 4001)
        //            {
        //                fwSqlDBType.dbType = SqlDbType.Binary;
        //            }
        //            else
        //            {
        //                fwSqlDBType.dbType = SqlDbType.VarBinary;
        //            }
        //            break;
        //        case OleDbType.Char:
        //            //
        //            // 摘要:
        //            //     字符串 (DBTYPE_STR)。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.Char;
        //            break;
        //        case OleDbType.WChar:
        //            //
        //            // 摘要:
        //            //     以 null 终止的 Unicode 字符流 (DBTYPE_WSTR)。它映射到 System.String。
        //            if (fwSqlDBType.length > 0 && fwSqlDBType.length < 4001)
        //            {
        //                fwSqlDBType.dbType = SqlDbType.NVarChar;
        //            }
        //            else
        //            {
        //                fwSqlDBType.dbType = SqlDbType.NText;
        //            }
        //            break;
        //        case OleDbType.Numeric:
        //            //
        //            // 摘要:
        //            //     具有定点精度和小数位数的精确数值 (DBTYPE_NUMERIC)。它映射到 System.Decimal。
        //            fwSqlDBType.dbType = SqlDbType.Decimal;
        //            break;
        //        case OleDbType.DBDate:
        //            //
        //            // 摘要:
        //            //     格式为 yyyymmdd 的日期数据 (DBTYPE_DBDATE)。它映射到 System.DateTime。
        //            fwSqlDBType.dbType = SqlDbType.DateTime;
        //            break;
        //        case OleDbType.DBTime:
        //            //
        //            // 摘要:
        //            //     格式为 hhmmss 的时间数据 (DBTYPE_DBTIME)。它映射到 System.TimeSpan。
        //            fwSqlDBType.dbType = SqlDbType.DateTime;
        //            break;
        //        case OleDbType.DBTimeStamp:
        //            //
        //            // 摘要:
        //            //     格式为 yyyymmddhhmmss 的日期和时间数据 (DBTYPE_DBTIMESTAMP)。它映射到 System.DateTime。
        //            fwSqlDBType.dbType = SqlDbType.DateTime;
        //            break;
        //        case OleDbType.PropVariant:
        //            //
        //            // 摘要:
        //            //     自动化 PROPVARIANT (DBTYPE_PROP_VARIANT)。它映射到 System.Object。
        //            fwSqlDBType.dbType = SqlDbType.Variant;
        //            break;
        //        case OleDbType.VarNumeric:
        //            //
        //            // 摘要:
        //            //     变长数值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Decimal。
        //            fwSqlDBType.dbType = SqlDbType.Decimal;
        //            break;
        //        case OleDbType.VarChar:
        //            //
        //            // 摘要:
        //            //     非 Unicode 字符的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.VarChar;
        //            break;
        //        case OleDbType.LongVarChar:
        //            //
        //            // 摘要:
        //            //     长的字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.NText;
        //            break;
        //        case OleDbType.VarWChar:
        //            //
        //            // 摘要:
        //            //     长可变、以 null 终止的 Unicode 字符流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.NText;
        //            break;
        //        case OleDbType.LongVarWChar:
        //            //
        //            // 摘要:
        //            //     长的以 null 终止的 Unicode 字符串值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.String。
        //            fwSqlDBType.dbType = SqlDbType.NText;
        //            break;
        //        case OleDbType.VarBinary:
        //            //
        //            // 摘要:
        //            //     二进制数据的变长流（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        //            fwSqlDBType.dbType = SqlDbType.VarBinary;
        //            break;
        //        case OleDbType.LongVarBinary:
        //            //
        //            // 摘要:
        //            //     长的二进制值（只限 System.Data.OleDb.OleDbParameter）。它映射到 System.Byte 类型的 System.Array。
        //            fwSqlDBType.dbType = SqlDbType.VarBinary;
        //            break;
        //        default:
        //            throw new Exception("SqlDbType中不存在OleDbType中值为" + fwOleDbDBType.dbType + "的映射对象");
        //            break;

        //    }
        //    return fwSqlDBType;
        //}
        //public static SqlDbTypeCustom getSqlDBType(FWMySqlDbType fwMySqlDBType)
        //{
        //    SqlDbTypeCustom fwSqlDbType = new SqlDbTypeCustom();
        //    fwSqlDbType.length = fwMySqlDBType.length;
        //    fwSqlDbType.precison = fwMySqlDBType.precison;
        //    fwSqlDbType.scale = fwMySqlDBType.scale;
        //    switch (fwMySqlDBType.dbType)
        //    {
        //        case MySqlDbType.Binary:
        //            fwSqlDbType.dbType = SqlDbType.Binary;
        //            break;
        //        case MySqlDbType.Bit:
        //            fwSqlDbType.dbType = SqlDbType.Bit;
        //            break;
        //        case MySqlDbType.Blob:
        //            fwSqlDbType.dbType = SqlDbType.Binary;
        //            break;
        //        case MySqlDbType.Byte:
        //            fwSqlDbType.dbType = SqlDbType.Image;
        //            break;
        //        case MySqlDbType.Date:
        //            fwSqlDbType.dbType = SqlDbType.Date;
        //            break;
        //        case MySqlDbType.DateTime:
        //            fwSqlDbType.dbType = SqlDbType.DateTime;
        //            break;
        //        case MySqlDbType.Decimal:
        //            fwSqlDbType.dbType = SqlDbType.Decimal;
        //            break;
        //        case MySqlDbType.Double:
        //            fwSqlDbType.dbType = SqlDbType.Float;
        //            break;
        //        case MySqlDbType.Enum:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Float:
        //            fwSqlDbType.dbType = SqlDbType.Float; ;
        //            break;
        //        case MySqlDbType.Geometry:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Int16:
        //            fwSqlDbType.dbType = SqlDbType.SmallInt;
        //            break;
        //        case MySqlDbType.Int24:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Int32:
        //            fwSqlDbType.dbType = SqlDbType.Int;
        //            break;
        //        case MySqlDbType.Int64:
        //            fwSqlDbType.dbType = SqlDbType.BigInt;
        //            break;
        //        case MySqlDbType.LongBlob:
        //            fwSqlDbType.dbType = SqlDbType.Binary;
        //            break;
        //        case MySqlDbType.LongText:
        //            fwSqlDbType.dbType = SqlDbType.Text;
        //            break;
        //        case MySqlDbType.MediumBlob:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.MediumText:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Newdate:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.NewDecimal:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Set:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.String:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Text:
        //            fwSqlDbType.dbType = SqlDbType.Text;
        //            break;
        //        case MySqlDbType.Time:
        //            fwSqlDbType.dbType = SqlDbType.Time;
        //            break;
        //        case MySqlDbType.Timestamp:
        //            fwSqlDbType.dbType = SqlDbType.Binary;
        //            break;
        //        case MySqlDbType.TinyBlob:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.TinyText:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.UByte:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.UInt16:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.UInt24:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.UInt32:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.UInt64:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.VarBinary:
        //            fwSqlDbType.dbType = SqlDbType.Binary;
        //            break;
        //        case MySqlDbType.VarChar:
        //            fwSqlDbType.dbType = SqlDbType.VarChar;
        //            break;
        //        case MySqlDbType.VarString:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //        case MySqlDbType.Year:
        //            //fwDBType.dbType = SqlDbType.;
        //            break;
        //    }
        //    return fwSqlDbType;
        //}
        //public static FWMySqlDbType getMySqlDBType(SqlDbTypeCustom fwSqlDBType)
        //{
        //    FWMySqlDbType fwMySqlDBType = new FWMySqlDbType();
        //    fwMySqlDBType.length = fwSqlDBType.length;
        //    fwMySqlDBType.precison = fwSqlDBType.precison;
        //    fwMySqlDBType.scale = fwSqlDBType.scale;
        //    switch (fwSqlDBType.dbType)
        //    {
        //        case SqlDbType.BigInt:
        //            // 摘要:
        //            //     System.Int64。64 位的有符号整数。
        //            fwMySqlDBType.dbType = MySqlDbType.Int64;
        //            break;
        //        case SqlDbType.Binary:
        //            //
        //            // 摘要:
        //            //     System.Byte 类型的 System.Array。二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。
        //            fwMySqlDBType.dbType = MySqlDbType.Binary;
        //            break;
        //        case SqlDbType.Bit:
        //            //
        //            // 摘要:
        //            //     System.Boolean。无符号数值，可以是 0、1 或 null。
        //            fwMySqlDBType.dbType = MySqlDbType.Bit;
        //            break;
        //        case SqlDbType.Char:
        //            //
        //            // 摘要:
        //            //     System.String。非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。
        //            fwMySqlDBType.dbType = MySqlDbType.Text;
        //            break;
        //        case SqlDbType.DateTime:
        //            //
        //            // 摘要:
        //            //     System.DateTime。日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。
        //            fwMySqlDBType.dbType = MySqlDbType.DateTime;
        //            break;
        //        case SqlDbType.Decimal:
        //            //
        //            // 摘要:
        //            //     System.Decimal。固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。
        //            fwMySqlDBType.dbType = MySqlDbType.Decimal;
        //            break;
        //        case SqlDbType.Float:
        //            //
        //            // 摘要:
        //            //     System.Double。-1.79E +308 到 1.79E +308 范围内的浮点数。
        //            fwMySqlDBType.dbType = MySqlDbType.Float;
        //            break;
        //        case SqlDbType.Image:
        //            //
        //            // 摘要:
        //            //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。
        //            fwMySqlDBType.dbType = MySqlDbType.LongBlob;
        //            break;
        //        case SqlDbType.Int:
        //            //
        //            // 摘要:
        //            //     System.Int32。32 位的有符号整数。
        //            fwMySqlDBType.dbType = MySqlDbType.Int32;
        //            break;
        //        case SqlDbType.Money:
        //            //
        //            // 摘要:
        //            //     System.Decimal。货币值，范围在 -2 63（即 -9,223,372,036,854,775,808）到 2 63 -1（即 +9,223,372,036,854,775,807）之间，精度为千分之十个货币单位。
        //            fwMySqlDBType.dbType = MySqlDbType.Decimal;
        //            break;
        //        case SqlDbType.NChar:
        //            //
        //            // 摘要:
        //            //     System.String。Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。
        //            fwMySqlDBType.dbType = MySqlDbType.VarChar;
        //            break;
        //        case SqlDbType.NText:
        //            //
        //            // 摘要:
        //            //     System.String。Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。
        //            fwMySqlDBType.dbType = MySqlDbType.LongText;
        //            break;
        //        case SqlDbType.NVarChar:
        //            //
        //            // 摘要:
        //            //     System.String。Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。如果字符串大于 4,000 个字符，隐式转换会失败。在使用比
        //            //     4,000 个字符更长的字符串时，请显式设置对象。
        //            fwMySqlDBType.dbType = MySqlDbType.VarChar;
        //            break;
        //        case SqlDbType.Real:
        //            //
        //            // 摘要:
        //            //     System.Single。-3.40E +38 到 3.40E +38 范围内的浮点数。
        //            break;
        //        case SqlDbType.UniqueIdentifier:
        //            //
        //            // 摘要:
        //            //     System.Guid。全局唯一标识符（或 GUID）。
        //            //fwMySqlDBType.dbType = MySqlDbType.Guid;
        //            break;
        //        case SqlDbType.SmallDateTime:
        //            //
        //            // 摘要:
        //            //     System.DateTime。日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。
        //            fwMySqlDBType.dbType = MySqlDbType.DateTime;
        //            break;
        //        case SqlDbType.SmallInt:
        //            //
        //            // 摘要:
        //            //     System.Int16。16 位的有符号整数。
        //            fwMySqlDBType.dbType = MySqlDbType.Int16;
        //            break;
        //        case SqlDbType.SmallMoney:
        //            //
        //            // 摘要:
        //            //     System.Decimal。货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。
        //            fwMySqlDBType.dbType = MySqlDbType.Decimal;
        //            break;
        //        case SqlDbType.Text:
        //            //
        //            // 摘要:
        //            //     System.String。非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。
        //            fwMySqlDBType.dbType = MySqlDbType.LongText;
        //            break;
        //        case SqlDbType.Timestamp:
        //            //
        //            // 摘要:
        //            //     System.Byte 类型的 System.Array。自动生成的二进制数，并保证其在数据库中唯一。timestamp 通常用作对表中各行的版本进行标记的机制。存储大小为
        //            //     8 字节。
        //            fwMySqlDBType.dbType = MySqlDbType.Timestamp;
        //            break;
        //        case SqlDbType.TinyInt:
        //            //
        //            // 摘要:
        //            //     System.Byte。8 位的无符号整数。
        //            fwMySqlDBType.dbType = MySqlDbType.Byte;
        //            break;
        //        case SqlDbType.VarBinary:
        //            //
        //            // 摘要:
        //            //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。如果字节数组大于 8,000
        //            //     个字节，隐式转换会失败。在使用比 8,000 个字节大的字节数组时，请显式设置对象。
        //            fwMySqlDBType.dbType = MySqlDbType.VarBinary;
        //            break;
        //        case SqlDbType.VarChar:
        //            //
        //            // 摘要:
        //            //     System.String。非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
        //            fwMySqlDBType.dbType = MySqlDbType.VarChar;
        //            break;
        //        case SqlDbType.Variant:
        //            //
        //            // 摘要:
        //            //     System.Object。特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。
        //            break;
        //        case SqlDbType.Xml:
        //            //
        //            // 摘要:
        //            //     XML 值。使用 System.Data.SqlClient.SqlDataReader.GetValue(System.Int32) 方法或 System.Data.SqlTypes.SqlXml.Value
        //            //     属性获取字符串形式的 XML，或通过调用 System.Data.SqlTypes.SqlXml.CreateReader() 方法获取 System.Xml.XmlReader
        //            //     形式的 XML。
        //            fwMySqlDBType.dbType = MySqlDbType.Text;
        //            break;
        //        case SqlDbType.Udt:
        //            //
        //            // 摘要:
        //            //     SQL Server 2005 用户定义的类型 (UDT)。
        //            break;
        //        case SqlDbType.Structured:
        //            //
        //            // 摘要:
        //            //     指定表值参数中包含的构造数据的特殊数据类型。
        //            break;
        //        case SqlDbType.Date:
        //            //
        //            // 摘要:
        //            //     日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。
        //            fwMySqlDBType.dbType = MySqlDbType.Date;
        //            break;
        //        case SqlDbType.Time:
        //            //
        //            // 摘要:
        //            //     基于 24 小时制的时间数据。时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。
        //            fwMySqlDBType.dbType = MySqlDbType.DateTime;
        //            break;
        //        case SqlDbType.DateTime2:
        //            //
        //            // 摘要:
        //            //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。
        //            break;
        //        case SqlDbType.DateTimeOffset:
        //            //
        //            // 摘要:
        //            //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
        //            //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
        //            break;
        //    }
        //    return fwMySqlDBType;
        //}



        //public static T getDBType<T>(IDbTypeCustom iFWDBType)
        //{
        //    Type iFWDBTypeType = iFWDBType.GetType();
        //    Type tType = typeof(T);
        //    if (iFWDBTypeType == tType)
        //    {
        //        return (T)iFWDBType;
        //    }
        //    T result = default(T);
        //    if (iFWDBTypeType == typeof(DbTypeCustom))
        //    {
        //        DbTypeCustom fwDBType = (DbTypeCustom)iFWDBType;
        //        if (typeof(T) == typeof(SqlDbTypeCustom))
        //        {
        //            result = (T)((object)getSqlDBType(fwDBType));
        //        }
        //        else if (typeof(T) == typeof(FWOleDbType))
        //        {
        //            result = (T)((object)getOleDbDBType(fwDBType));
        //        }
        //        else if (typeof(T) == typeof(FWMySqlDbType))
        //        {
        //            result = (T)((object)getMySqlDBType(fwDBType));
        //        }
        //        else
        //        {
        //            throw new Exception("泛型对象只能是FWSqlDBType、FWOracleDBType、FWOdbcDBType、FWOleDbDBType其中一种");
        //        }
        //    }
        //    else if (iFWDBTypeType == typeof(FWOleDbType))
        //    {
        //        FWOleDbType fwOleDbDBType = (FWOleDbType)iFWDBType;
        //        if (typeof(T) == typeof(SqlDbTypeCustom))
        //        {
        //            result = (T)((object)getSqlDBType(fwOleDbDBType));
        //        }
        //        else
        //        {
        //            throw new Exception("泛型对象只能是FWSqlDBType、FWOracleDBType、FWOdbcDBType、FWOleDbDBType其中一种");
        //        }
        //    }
        //    else if (iFWDBTypeType == typeof(SqlDbTypeCustom))
        //    {
        //        SqlDbTypeCustom fwSqlDBType = (SqlDbTypeCustom)iFWDBType;
        //        if (typeof(T) == typeof(DbTypeCustom))
        //        {
        //            result = (T)((object)getDBType(fwSqlDBType));
        //        }
        //        else if (typeof(T) == typeof(FWMySqlDbType))
        //        {
        //            result = (T)((object)getMySqlDBType(fwSqlDBType));
        //        }
        //        else
        //        {
        //            throw new Exception("泛型对象只能是FWSqlDBType、FWOracleDBType、FWOdbcDBType、FWOleDbDBType其中一种");
        //        }
        //    }
        //    else if (iFWDBTypeType == typeof(FWMySqlDbType))
        //    {
        //        FWMySqlDbType fwMySqlDBType = (FWMySqlDbType)iFWDBType;
        //        if (typeof(T) == typeof(DbTypeCustom))
        //        {
        //            result = (T)((object)getDBType(fwMySqlDBType));
        //        }
        //        else if (typeof(T) == typeof(SqlDbTypeCustom))
        //        {
        //            result = (T)((object)getSqlDBType(fwMySqlDBType));
        //        }
        //        else
        //        {
        //            throw new Exception("泛型对象只能是FWSqlDBType、FWOracleDBType、FWOdbcDBType、FWOleDbDBType其中一种");
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("参数fwDBType只能是FWSqlDBType、FWOracleDBType、FWOdbcDBType、FWOleDbDBType其中一种");
        //    }
        //    return result;
        //}
        #endregion

    }
}
