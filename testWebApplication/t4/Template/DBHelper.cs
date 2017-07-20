using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace testWebApplication.t4.Template
{
    public class DBHelper
    {

    }
    public class DBSchemaFactory
    {
        static readonly string DatabaseType = "SqlServer";
        public static IDBSchema GetDBSchema()
        {
            IDBSchema dbSchema;
            switch (DatabaseType)
            {
                case "SqlServer":
                    {
                        dbSchema = new SqlServerSchema();
                        break;
                    }
                case "MySql":
                    {
                        dbSchema = new MySqlSchema();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("The input argument of DatabaseType is invalid!");
                    }
            }
            return dbSchema;
        }
    }

    public interface IDBSchema : IDisposable
    {
        List<string> GetTablesList();

        Table GetTableMetadata(string tableName);
    }

    public class SqlServerSchema : IDBSchema
    {
        public string ConnectionString = "Data Source=.;Initial Catalog=DB_YDZF_Product;Persist Security Info=True;User ID=sa;Password=sckj;";

        public SqlConnection conn;

        public SqlServerSchema()
        {
            conn = new SqlConnection(ConnectionString);
            conn.Open();
        }

        public List<string> GetTablesList()
        {
            DataTable dt = conn.GetSchema("Tables");
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["TABLE_NAME"].ToString());
            }
            return list;
        }

        public Table GetTableMetadata(string tableName)
        {
            string selectCmdText = string.Format("SELECT * FROM {0}", tableName); ;
            SqlCommand command = new SqlCommand(selectCmdText, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            ad.FillSchema(ds, SchemaType.Mapped, tableName);

            Table table = new Table(ds.Tables[0]);
            SetTableColumnDesc(tableName, table);
            return table;
        }

        private DataTable getDataTable(string sqlString)
        {
            SqlCommand command = new SqlCommand(sqlString, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            ad.FillSchema(ds, SchemaType.Mapped);
            return ds.Tables[0];
        }

        public void SetTableColumnDesc(string tableName, Table table)
        {
            string sql = string.Format(@"
SELECT  tableName = CASE WHEN a.colorder = 1 THEN d.name ELSE ''  END ,
        tableDesc = CASE WHEN a.colorder = 1 THEN ISNULL(f.value, '') ELSE '' END ,
        columnNumber = a.colorder ,
        columnName = a.name ,
        isIdentity = CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END,
        isPrimary = CASE WHEN EXISTS ( SELECT  1 FROM    sysobjects WHERE   xtype = 'PK'    AND parent_obj = a.id    AND name IN (    SELECT  name    FROM    sysindexes    WHERE   indid IN (            SELECT  indid            FROM    sysindexkeys            WHERE   id = a.id       AND colid = a.colid ) ) )     THEN CAST(1 AS BIT)     ELSE CAST(0 AS BIT)             END ,
        typeName = b.name ,
        /*length = a.length ,*/
        intLength = COLUMNPROPERTY(a.id, a.name, 'PRECISION') ,
        digitLength = ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) ,
        isRequired = CASE WHEN a.isnullable = 1 THEN CAST(1 AS bit) ELSE CAST(0 AS BIT) END ,
        defaultValue = ISNULL(e.text, '') ,
        columnDesc = ISNULL(g.[value], '')
FROM    syscolumns a
        LEFT JOIN systypes b ON a.xusertype = b.xusertype
        INNER JOIN sysobjects d ON a.id = d.id
    AND d.xtype = 'U'
    AND d.name <> 'dtproperties'
        LEFT JOIN syscomments e ON a.cdefault = e.id
        LEFT JOIN sys.extended_properties g ON a.id = g.major_id
           AND a.colid = g.minor_id
        LEFT JOIN sys.extended_properties f ON d.id = f.major_id
           AND f.minor_id = 0
WHERE   d.name = '{0}'    --如果只查询指定表,加上此红色where条件，tablename是要查询的表名；去除红色where条件查询说有的表信息
ORDER BY a.id ,
        a.colorder", tableName);

        }

        public void Dispose()
        {
            if (conn != null)
                conn.Close();
        }
    }

    public class MySqlSchema : IDBSchema
    {
        public string ConnectionString = "Server=localhost;Port=3306;Database=ProjectData;Uid=root;Pwd=;";

        public MySqlConnection conn;

        public MySqlSchema()
        {
            conn = new MySqlConnection(ConnectionString);
            conn.Open();
        }

        public List<string> GetTablesList()
        {
            DataTable dt = conn.GetSchema("Tables");
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["TABLE_NAME"].ToString());
            }
            return list;
        }

        public Table GetTableMetadata(string tableName)
        {
            string selectCmdText = string.Format("SELECT * FROM {0}", tableName); ;
            MySqlCommand command = new MySqlCommand(selectCmdText, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            ad.FillSchema(ds, SchemaType.Mapped, tableName);

            Table table = new Table(ds.Tables[0]);
            return table;
        }

        public void Dispose()
        {
            if (conn != null)
                conn.Close();
        }
    }

    public class Table
    {
        public Table(DataTable t)
        {
            this.PKs = this.GetPKList(t);
            this.Columns = this.GetColumnList(t);
            this.ColumnTypeNames = this.SetColumnNames();
        }

        public List<Column> PKs;

        public List<Column> Columns;

        public string ColumnDesc { get; set; }

        public string ColumnTypeNames;
        public List<Column> GetPKList(DataTable dt)
        {
            List<Column> list = new List<Column>();
            Column c = null;
            if (dt.PrimaryKey.Length > 0)
            {
                list = new List<Column>();
                foreach (DataColumn dc in dt.PrimaryKey)
                {
                    c = new Column(dc);
                    list.Add(c);
                }
            }
            return list;
        }

        private List<Column> GetColumnList(DataTable dt)
        {
            List<Column> list = new List<Column>();
            Column c = null;
            foreach (DataColumn dc in dt.Columns)
            {
                c = new Column(dc);
                list.Add(c);
            }
            return list;
        }

        private string SetColumnNames()
        {
            List<string> list = new List<string>();
            foreach (Column c in this.Columns)
            {
                list.Add(string.Format("{0} {1}", c.TypeName, c.LowerColumnName));
            }
            return string.Join(",", list.ToArray());
        }
    }

    public class Column
    {
        DataColumn columnBase;

        public Column(DataColumn columnBase)
        {
            this.columnBase = columnBase;
        }

        public string ColumnName { get { return this.columnBase.ColumnName; } }

        public string MaxLength { get { return this.columnBase.MaxLength.ToString(); } }

        public string TypeName
        {
            get
            {
                string result = string.Empty;
                if (this.columnBase.DataType.Name == "Guid")//for mysql,因为对于MYSQL如果是CHAR(36),类型自动为Guid
                    result = "String";
                else
                    result = this.columnBase.DataType.Name;
                return result;
            }
        }

        public bool AllowDBNull { get { return this.columnBase.AllowDBNull; } }

        public string UpColumnName
        {
            get
            {
                return string.Format("{0}{1}", this.ColumnName[0].ToString().ToUpper(), this.ColumnName.Substring(1));
            }
        }

        public string LowerColumnName
        {
            get
            {
                return string.Format("{0}{1}", this.ColumnName[0].ToString().ToLower(), this.ColumnName.Substring(1));
            }
        }

        public string ColumnDesc { get; set; }
    }

    public class GeneratorHelper
    {
        public static readonly string StringType = "String";
        public static readonly string DateTimeType = "DateTime";
        public static string GetQuesMarkByType(string typeName)
        {
            string result = typeName;
            if (typeName == DateTimeType)
            {
                result += "?";
            }
            return result;
        }
    }
}