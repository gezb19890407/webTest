using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Text;

namespace System.Data
{
    public class SqlCommandHelper : ICommandCustomHelper
    {

        public static string connectionString
        {
            get
            {
                return ConfigHelper.getValue("connectionString");//wangpinghua
            }
        }

        private string _connectionString
        {
            get
            {
                return connectionString;
            }
        }
        private IConnectionCustom __dbConnection = null;
        private IConnectionCustom _dbConnection
        {
            get
            {
                if (__dbConnection == null)
                {
                    if (_connectionString == null)
                    {
                        throw new Exception("数据库连接字符串不存在");
                    }
                    else
                    {
                        __dbConnection = new ConnectionCustom(_connectionString);
                    }
                }
                return __dbConnection;
            }
        }


        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get { return DatabaseTypeCustom.Sql; }
        }

        public bool isCanConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool isCanConnection()
        {
            return isCanConnection((new SqlConnectionCustom()).connectionString);
        }

        public string checkParam(string param)
        {
            if (param == null)
            {
                return "";
            }
            else
            {
                //删除脚本
                param = param.Replace("\r\n", " ");
                param = Regex.Replace(param, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"<.*?>", "", RegexOptions.IgnoreCase);
                //删除HTML
                param = Regex.Replace(param, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"-->", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"<!--.*", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                //param = param.Replace("--", "");
                param = param.Replace(";", "；");
                //删除与数据库相关的词
                param = Regex.Replace(param, "drop table", "d", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, "truncate", "t", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, " mid ", "m", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, " xp_cmdshell ", "x", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, " exec master ", "e", RegexOptions.IgnoreCase);
                param = Regex.Replace(param, " net localgroup administrators ", "n", RegexOptions.IgnoreCase);
                param = param.Replace("=", " = ");
                param = param.Replace("\n", " ");
                param = param.Replace("'", "''");
                return param;

            }
        }

        public string joinToSqlString<T>(List<T> tList)
        {
            StringBuilder sb = new StringBuilder();
            if (tList != null && tList.Count > 0)
            {
                string tString;
                bool isNumber = typeof(T) == typeof(Int32) || typeof(T) == typeof(Int64) || typeof(T) == typeof(Int16) || typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal);
                foreach (T t in tList)
                {
                    tString = t.ToString();
                    if (!String.IsNullOrEmpty(tString))
                    {
                        tString = checkParam(tString);
                        if (isNumber)
                        {
                            sb.Append(tString + ",");
                        }
                        else
                        {
                            sb.Append("'" + checkParam(tString) + "'" + ",");
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                else
                {
                    sb.Append("''");
                }
            }
            return sb.ToString();
        }

        public string getDbTypeString(IDbTypeCustom dbType)
        {
            string result = "";
            #region 停用 待完善

            //SqlDbTypeCustom fwSqlDBType = DbTypeHelper.getDbType<SqlDbTypeCustom>(dbType);
            //switch (fwSqlDBType.dbType)
            //{
            //    // 摘要:
            //    //     System.Int64。64 位的有符号整数。
            //    case SqlDbType.BigInt:
            //        result = "[bigint]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。
            //    case SqlDbType.Binary:
            //        result = "[varbinary](" + fwSqlDBType.length + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Boolean。无符号数值，可以是 0、1 或 null。
            //    case SqlDbType.Bit:
            //        result = "[bit]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。
            //    case SqlDbType.Char:
            //        result = "[char](" + fwSqlDBType.length + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.DateTime。日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。
            //    case SqlDbType.DateTime:
            //        result = "[datetime]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。
            //    case SqlDbType.Decimal:
            //        result = "[decimal](" + fwSqlDBType.precison + ", " + fwSqlDBType.scale + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Double。-1.79E +308 到 1.79E +308 范围内的浮点数。
            //    case SqlDbType.Float:
            //        result = "[float]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。
            //    case SqlDbType.Image:
            //        result = "[image]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Int32。32 位的有符号整数。
            //    case SqlDbType.Int:
            //        result = "[int]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。货币值，范围在 -2 63（即 -9,223,372,036,854,775,808）到 2 63 -1（即 +9,223,372,036,854,775,807）之间，精度为千分之十个货币单位。
            //    case SqlDbType.Money:
            //        result = "[money]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。
            //    case SqlDbType.NChar:
            //        result = "[nchar](" + fwSqlDBType.length / 2 + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。
            //    case SqlDbType.NText:
            //        result = "[ntext]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。如果字符串大于 4,000 个字符，隐式转换会失败。在使用比
            //    //     4,000 个字符更长的字符串时，请显式设置对象。
            //    case SqlDbType.NVarChar:
            //        if (fwSqlDBType.length < 1)
            //        {
            //            result = "[nvarchar](max)";
            //        }
            //        else
            //        {
            //            result = "[nvarchar](" + fwSqlDBType.length / 2 + ")";
            //        }
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Single。-3.40E +38 到 3.40E +38 范围内的浮点数。
            //    case SqlDbType.Real:
            //        result = "[real]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Guid。全局唯一标识符（或 GUID）。
            //    case SqlDbType.UniqueIdentifier:
            //        result = "[uniqueidentifier]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.DateTime。日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。
            //    case SqlDbType.SmallDateTime:
            //        result = "[smalldatetime]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Int16。16 位的有符号整数。
            //    case SqlDbType.SmallInt:
            //        result = "[smallint]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。
            //    case SqlDbType.SmallMoney:
            //        result = "[smallmoney]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。
            //    case SqlDbType.Text:
            //        result = "[text]";
            //        break;
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。自动生成的二进制数，并保证其在数据库中唯一。timestamp 通常用作对表中各行的版本进行标记的机制。存储大小为
            //    //     8 字节。
            //    case SqlDbType.Timestamp:
            //        result = "[timestamp]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte。8 位的无符号整数。
            //    case SqlDbType.TinyInt:
            //        result = "[tinyint]";
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。如果字节数组大于 8,000
            //    //     个字节，隐式转换会失败。在使用比 8,000 个字节大的字节数组时，请显式设置对象。
            //    case SqlDbType.VarBinary:
            //        if (fwSqlDBType.length > 0 && fwSqlDBType.length < 8001)
            //        {
            //            result = "[varbinary](" + fwSqlDBType.length + ")";
            //        }
            //        else
            //        {
            //            result = "[varbinary](max)";
            //        }
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
            //    case SqlDbType.VarChar:
            //        if (fwSqlDBType.length < 1)
            //        {
            //            result = "[varchar](max)";
            //        }
            //        else
            //        {
            //            result = "[varchar](" + fwSqlDBType.length + ")";
            //        }
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Object。特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。
            //    case SqlDbType.Variant:
            //        break;
            //    //
            //    // 摘要:
            //    //     XML 值。使用 System.Data.SqlClient.SqlDataReader.GetValue(System.Int32) 方法或 System.Data.SqlTypes.SqlXml.Value
            //    //     属性获取字符串形式的 XML，或通过调用 System.Data.SqlTypes.SqlXml.CreateReader() 方法获取 System.Xml.XmlReader
            //    //     形式的 XML。
            //    case SqlDbType.Xml:
            //        result = "[xml]";
            //        break;
            //    //
            //    // 摘要:
            //    //     SQL Server 2005 用户定义的类型 (UDT)。
            //    case SqlDbType.Udt:
            //        break;
            //    //
            //    // 摘要:
            //    //     指定表值参数中包含的构造数据的特殊数据类型。
            //    case SqlDbType.Structured:
            //        break;
            //    //
            //    // 摘要:
            //    //     日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。
            //    case SqlDbType.Date:
            //        result = "[date]";
            //        break;
            //    //
            //    // 摘要:
            //    //     基于 24 小时制的时间数据。时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。
            //    case SqlDbType.Time:
            //        result = "[time](" + fwSqlDBType.scale + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
            //    //     100 毫微秒。
            //    case SqlDbType.DateTime2:
            //        result = "[datetime2](" + fwSqlDBType.scale + ")";
            //        break;
            //    //
            //    // 摘要:
            //    //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
            //    //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
            //    case SqlDbType.DateTimeOffset:
            //        result = "[datetimeoffset](" + fwSqlDBType.scale + ")";
            //        break;
            //}
            #endregion
            if (string.IsNullOrEmpty(result))
            {
                //throw new Exception("完善类型为" + fwSqlDBType.dbType + "映射的sqlDbType字符串");
            }
            return result;
        }

        public Type getValueType(IDbTypeCustom dbType)
        {
            Type result = null;
            #region 停用 待完善

            //SqlDbTypeCustom fwSqlDBType = DbTypeHelper.getDbType<SqlDbTypeCustom>(dbType);
            //switch (fwSqlDBType.dbType)
            //{
            //    // 摘要:
            //    //     System.Int64。64 位的有符号整数。
            //    case SqlDbType.BigInt:
            //        result = typeof(Int64);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的固定长度流，范围在 1 到 8,000 个字节之间。
            //    case SqlDbType.Binary:
            //        result = typeof(Byte[]);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Boolean。无符号数值，可以是 0、1 或 null。
            //    case SqlDbType.Bit:
            //        result = typeof(Boolean);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 字符的固定长度流，范围在 1 到 8,000 个字符之间。
            //    case SqlDbType.Char:
            //        result = typeof(String);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.DateTime。日期和时间数据，值范围从 1753 年 1 月 1 日到 9999 年 12 月 31 日，精度为 3.33 毫秒。
            //    case SqlDbType.DateTime:
            //        result = typeof(DateTime);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。固定精度和小数位数数值，在 -10 38 -1 和 10 38 -1 之间。
            //    case SqlDbType.Decimal:
            //        result = typeof(Decimal);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Double。-1.79E +308 到 1.79E +308 范围内的浮点数。
            //    case SqlDbType.Float:
            //        result = typeof(Double);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 0 到 2 31 -1（即 2,147,483,647）字节之间。
            //    case SqlDbType.Image:
            //        result = typeof(Byte[]);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Int32。32 位的有符号整数。
            //    case SqlDbType.Int:
            //        result = typeof(Int32);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。货币值，范围在 -2 63（即 -9,223,372,036,854,775,808）到 2 63 -1（即 +9,223,372,036,854,775,807）之间，精度为千分之十个货币单位。
            //    case SqlDbType.Money:
            //        result = typeof(Decimal);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 字符的固定长度流，范围在 1 到 4,000 个字符之间。
            //    case SqlDbType.NChar:
            //        result = typeof(String);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 数据的可变长度流，最大长度为 2 30 - 1（即 1,073,741,823）个字符。
            //    case SqlDbType.NText:
            //        result = typeof(String);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。Unicode 字符的可变长度流，范围在 1 到 4,000 个字符之间。如果字符串大于 4,000 个字符，隐式转换会失败。在使用比
            //    //     4,000 个字符更长的字符串时，请显式设置对象。
            //    case SqlDbType.NVarChar:
            //        result = typeof(String);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Single。-3.40E +38 到 3.40E +38 范围内的浮点数。
            //    case SqlDbType.Real:
            //        result = typeof(Single);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Guid。全局唯一标识符（或 GUID）。
            //    case SqlDbType.UniqueIdentifier:
            //        result = typeof(Guid);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.DateTime。日期和时间数据，值范围从 1900 年 1 月 1 日到 2079 年 6 月 6 日，精度为 1 分钟。
            //    case SqlDbType.SmallDateTime:
            //        result = typeof(DateTime);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Int16。16 位的有符号整数。
            //    case SqlDbType.SmallInt:
            //        result = typeof(Int16);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Decimal。货币值，范围在 -214,748.3648 到 +214,748.3647 之间，精度为千分之十个货币单位。
            //    case SqlDbType.SmallMoney:
            //        result = typeof(Decimal);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 数据的可变长度流，最大长度为 2 31 -1（即 2,147,483,647）个字符。
            //    case SqlDbType.Text:
            //        result = typeof(String);
            //        break;
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。自动生成的二进制数，并保证其在数据库中唯一。timestamp 通常用作对表中各行的版本进行标记的机制。存储大小为
            //    //     8 字节。
            //    case SqlDbType.Timestamp:
            //        result = typeof(Byte[]);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte。8 位的无符号整数。
            //    case SqlDbType.TinyInt:
            //        result = typeof(Byte);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Byte 类型的 System.Array。二进制数据的可变长度流，范围在 1 到 8,000 个字节之间。如果字节数组大于 8,000
            //    //     个字节，隐式转换会失败。在使用比 8,000 个字节大的字节数组时，请显式设置对象。
            //    case SqlDbType.VarBinary:
            //        result = typeof(Byte[]);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.String。非 Unicode 字符的可变长度流，范围在 1 到 8,000 个字符之间。
            //    case SqlDbType.VarChar:
            //        result = typeof(String);
            //        break;
            //    //
            //    // 摘要:
            //    //     System.Object。特殊数据类型，可以包含数值、字符串、二进制或日期数据，以及 SQL Server 值 Empty 和 Null，后两个值在未声明其他类型的情况下采用。
            //    case SqlDbType.Variant:
            //        result = typeof(Object);
            //        break;
            //    //
            //    // 摘要:
            //    //     XML 值。使用 System.Data.SqlClient.SqlDataReader.GetValue(System.Int32) 方法或 System.Data.SqlTypes.SqlXml.Value
            //    //     属性获取字符串形式的 XML，或通过调用 System.Data.SqlTypes.SqlXml.CreateReader() 方法获取 System.Xml.XmlReader
            //    //     形式的 XML。
            //    case SqlDbType.Xml:
            //        //result = typeof(Xml);
            //        break;
            //    //
            //    // 摘要:
            //    //     SQL Server 2005 用户定义的类型 (UDT)。
            //    case SqlDbType.Udt:
            //        break;
            //    //
            //    // 摘要:
            //    //     指定表值参数中包含的构造数据的特殊数据类型。
            //    case SqlDbType.Structured:
            //        break;
            //    //
            //    // 摘要:
            //    //     日期数据，值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。
            //    case SqlDbType.Date:
            //        result = typeof(DateTime);
            //        break;
            //    //
            //    // 摘要:
            //    //     基于 24 小时制的时间数据。时间值范围从 00:00:00 到 23:59:59.9999999，精度为 100 毫微秒。
            //    case SqlDbType.Time:
            //        result = typeof(DateTime);
            //        break;
            //    //
            //    // 摘要:
            //    //     日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
            //    //     100 毫微秒。
            //    case SqlDbType.DateTime2:
            //        result = typeof(DateTime);
            //        break;
            //    //
            //    // 摘要:
            //    //     显示时区的日期和时间数据。日期值范围从公元 1 年 1 月 1 日到公元 9999 年 12 月 31 日。时间值范围从 00:00:00 到 23:59:59.9999999，精度为
            //    //     100 毫微秒。时区值范围从 -14:00 到 +14:00。
            //    case SqlDbType.DateTimeOffset:
            //        result = typeof(DateTime);
            //        break;
            //}
            #endregion
            if (result == null)
            {
                //throw new Exception("完善类型为" + fwSqlDBType.dbType + "映射的ValueType字符串");
            }
            return result;
        }

        public int ExecuteScalar(ICommandCustom Command)
        {
            return ExecuteScalar(string.Empty, Command);
        }

        public int ExecuteScalar(string connectionString, ICommandCustom Command)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                return ExecuteScalar(new SqlConnectionCustom(Connection), Command);
            }
        }

        public int ExecuteScalar(IConnectionCustom Connection, ICommandCustom Command)
        {
            if (Connection == null)
            {
                Connection = _dbConnection;
            }
            Command.IDbCommand.CommandTimeout = 600;
            PrepareCommand(Command.IDbCommand, Connection.iDbConnection, (SqlTransaction)null);
            int retval = -1;
            try
            {
                retval = Convert.ToInt32(Command.IDbCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Command.IDbCommand.Parameters.Clear();
            if (Connection.iDbConnection.State != ConnectionState.Closed)
            {
                Connection.iDbConnection.Close();
            }
            return retval;
        }

        public int ExecuteScalar(ITransactionCustom Transaction, ICommandCustom Command)
        {
            if (Transaction == null)
            {
                return ExecuteScalar(Command);
            }
            Command.IDbCommand.CommandTimeout = 600;
            PrepareCommand(Command.IDbCommand, Transaction.IDbTransaction.Connection, Transaction.IDbTransaction);
            int retval = -1;
            try
            {
                retval = Convert.ToInt32(Command.IDbCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
            Command.IDbCommand.Parameters.Clear();
            return retval;
        }

        public int ExecuteNonQuery(ICommandCustom Command)
        {
            return ExecuteNonQuery(string.Empty, Command);
        }

        public int ExecuteNonQuery(string connectionString, ICommandCustom Command)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                return ExecuteNonQuery(new SqlConnectionCustom(Connection), Command);
            }
        }

        public int ExecuteNonQuery(IConnectionCustom Connection, ICommandCustom Command)
        {
            if (Connection == null)
            {
                Connection = _dbConnection;
            }
            Command.IDbCommand.CommandTimeout = 600;
            PrepareCommand(Command.IDbCommand, Connection.iDbConnection, (SqlTransaction)null);
            int retval = -1;
            try
            {
                retval = Command.IDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Command.IDbCommand.Parameters.Clear();
            if (Connection.iDbConnection.State != ConnectionState.Closed)
            {
                Connection.iDbConnection.Close();
            }
            return retval;
        }

        public int ExecuteNonQuery(ITransactionCustom Transaction, ICommandCustom Command)
        {
            if (Transaction == null)
            {
                return ExecuteNonQuery(Command);
            }
            Command.IDbCommand.CommandTimeout = 600;
            PrepareCommand(Command.IDbCommand, Transaction.IDbTransaction.Connection, Transaction.IDbTransaction);
            int retval = -1;
            try
            {
                retval = Command.IDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
            Command.IDbCommand.Parameters.Clear();
            return retval;
        }

        public DataSet ExecuteDataSet(IDbCommand Command)
        {
            return ExecuteDataSet(string.Empty, Command);
        }

        public DataSet ExecuteDataSet(ICommandCustom Command)
        {
            return ExecuteDataSet(string.Empty, Command);
        }

        public DataSet ExecuteDataSet(string connectionString, IDbCommand Command)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                return ExecuteDataSet(Connection, Command);
            }
        }

        public DataSet ExecuteDataSet(string connectionString, ICommandCustom Command)
        {
            return ExecuteDataSet(connectionString, Command.IDbCommand);
        }

        public DataSet ExecuteDataSet(IDbConnection Connection, IDbCommand Command)
        {
            if (Connection == null)
            {
                Connection = _dbConnection.iDbConnection;
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Connection, (SqlTransaction)null);

            #region 拦截查询的SQL语句(仅当在Debug模式下执行)
#if DEBUG
            //if (ConfigHelper.getValue("isSqlFiddler") == "1")
            //{
            //    Dictionary<string, object> dicParameters = new Dictionary<string, object>();
            //    foreach (SqlParameter parameter in Command.Parameters)
            //    {
            //        dicParameters.Add(parameter.ParameterName, parameter.Value);
            //    }

            //    var sqlInfo = new
            //    {
            //        commandText = Command.CommandText,
            //        parameters = dicParameters,
            //        requestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            //    };
            //    string sqlInfoJson = fw.fwJson.FWJsonHelper.serializeObject(sqlInfo);
            //    FWSqlFiddler._fiddlerQueue.Enqueue(sqlInfoJson);
            //}
#endif
            #endregion

            using (SqlDataAdapter da = new SqlDataAdapter((SqlCommand)Command))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                Command.Parameters.Clear();
                if (Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
                return ds;
            }
        }

        public DataSet ExecuteDataSet(IConnectionCustom Connection, ICommandCustom Command)
        {
            return ExecuteDataSet(Connection.iDbConnection, Command.IDbCommand);
        }

        public DataSet ExecuteDataSet(IDbTransaction Transaction, IDbCommand Command)
        {
            if (Transaction == null)
            {
                return ExecuteDataSet(Command);
            }
            Command.CommandTimeout = 600;
            PrepareCommand(Command, Transaction.Connection, Transaction);
            try
            {
                using (SqlDataAdapter da = new SqlDataAdapter((SqlCommand)Command))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Command.Parameters.Clear();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(ITransactionCustom Transaction, ICommandCustom Command)
        {
            if (Transaction == null)
            {
                return ExecuteDataSet(string.Empty, Command.IDbCommand);
            }
            return ExecuteDataSet(Transaction.IDbTransaction, Command.IDbCommand);
        }

        public DataTable ExecuteDataTable(ICommandCustom Command)
        {
            return ExecuteDataTable(string.Empty, Command);
        }

        public DataTable ExecuteDataTable(string connectionString, ICommandCustom Command)
        {
            return ExecuteDataSet(connectionString, Command).Tables[0];
        }

        public DataTable ExecuteDataTable(IConnectionCustom Connection, ICommandCustom Command)
        {
            return ExecuteDataSet(Connection, Command).Tables[0];
        }

        public DataTable ExecuteDataTable(ITransactionCustom Transaction, ICommandCustom Command)
        {
            return ExecuteDataSet(Transaction, Command).Tables[0];
        }

        public bool ExecuteNonQuery(List<ICommandCustom> CommandList)
        {
            return ExecuteNonQuery(string.Empty, CommandList);
        }

        public bool ExecuteNonQuery(string connectionString, List<ICommandCustom> CommandList)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new Exception("数据库连接字符串不存在");
            }
            if (CommandList == null)
            {
                throw new Exception("SqlBaseCommand不存在");
            }
            if (CommandList.Count == 0)
            {
                throw new Exception("SqlBaseCommand只有O条");
            }
            if (CommandList.Count == 1)
            {
                //modify by lzc 20170710
                return (CommandList[0]).comparison(ExecuteNonQuery(connectionString, CommandList[0]));
            }
            else
            {
                SqlTransactionCustom Transaction = new SqlTransactionCustom(connectionString);
                Transaction.BeginTransaction();
                try
                {
                    SqlCommandCustom Command;
                    for (int i = 0; i < CommandList.Count; i++)
                    {
                        Command = ((SqlCommandCustom)CommandList[i]);
                        Command.ComparisonTypeCustom = ComparisonTypeCustom.ThanEqual;
                        if (!Command.comparison(ExecuteNonQuery(Transaction, Command)))
                        {
                            Transaction.Rollback();
                            return false;
                        }
                    }
                    Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw ex;
                }
            }
        }

        public bool ExecuteNonQuery(IConnectionCustom Connection, List<ICommandCustom> CommandList)
        {
            if (Connection == null)
            {
                Connection = _dbConnection;
            }
            if (Connection == null)
            {
                throw new Exception("数据库连接SqlConnection对象不存在");
            }
            if (CommandList == null)
            {
                throw new Exception("SqlBaseCommand不存在");
            }
            if (CommandList.Count == 0)
            {
                throw new Exception("SqlBaseCommand只有O条");
            }
            if (CommandList.Count == 1)
            {
                return ((CommandCustom)CommandList[0]).comparison(ExecuteNonQuery(Connection, CommandList[0]));
            }
            else
            {
                SqlTransactionCustom Transaction = new SqlTransactionCustom(Connection.iDbConnection);
                Transaction.BeginTransaction();
                try
                {
                    CommandCustom Command;
                    for (int i = 0; i < CommandList.Count; i++)
                    {
                        Command = ((CommandCustom)CommandList[i]);
                        Command.ComparisonTypeCustom = ComparisonTypeCustom.ThanEqual;
                        if (!Command.comparison(ExecuteNonQuery(Transaction, Command)))
                        {
                            Transaction.Rollback();
                            return false;
                        }
                    }
                    Transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw ex;
                }
            }
        }

        public bool ExecuteNonQuery(ITransactionCustom Transaction, List<ICommandCustom> CommandList)
        {
            if (Transaction == null)
            {
                return ExecuteNonQuery(string.Empty, CommandList);
            }
            if (CommandList == null)
            {
                throw new Exception("SqlBaseCommand不存在");
            }
            if (CommandList.Count == 0)
            {
                throw new Exception("SqlBaseCommand只有O条");
            }
            if (CommandList.Count == 1)
            {
                return ((SqlCommandCustom)CommandList[0]).comparison(ExecuteNonQuery(Transaction, CommandList[0]));
            }
            else
            {
                try
                {
                    CommandCustom Command;
                    for (int i = 0; i < CommandList.Count; i++)
                    {
                        Command = ((CommandCustom)CommandList[i]);
                        Command.ComparisonTypeCustom = ComparisonTypeCustom.ThanEqual;
                        if (!Command.comparison(ExecuteNonQuery(Transaction, Command)))
                        {
                            Transaction.Rollback();
                            return false;
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw ex;
                }
            }
        }

        private void PrepareCommand(IDbCommand Command, IDbConnection Connection, IDbTransaction Transaction)
        {
            if (Command == null)
            {
                throw new Exception("数据库命令command对象不存在");
            }
            Command.Connection = Connection;
            if (Transaction != null)
            {
                if (Transaction.Connection == null)
                {
                    throw new Exception("数据库连接字符串不存在");
                }
                Command.Transaction = Transaction;
            }
        }




    }
}
