using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.sqlError
{
    public partial class sqlErrorDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ExcuteClass<TestTmp> entity = new ExcuteClass<TestTmp>()
                {
                    ConnectionString = "server=.;database=AvaBettyDB;uid=sa;pwd=123456",
                    AccountId = "15",
                    MethodName = "SaveTestTmp",
                    Description = "报错错误",
                    ListWhereColumns = new List<string>() { "code", "value" },
                    ListEntity = new List<TestTmp>()
                {
                    new TestTmp()
                    {
                        code = "11111111",
                        value = "1",
                        updateTime = DateTime.Now
                    },
                    new TestTmp()
                    {
                        code = "2",
                        value = "3",
                        updateTime = DateTime.Now
                    }
                }
                };
                List<TestTmp> listEntity = new List<TestTmp>();
                TestTmp tmpEntity;
                for (int i = 0; i < 300; i++)
                {
                    tmpEntity = new TestTmp();
                    tmpEntity.code = i + "00";
                    tmpEntity.value = i.ToString();
                    listEntity.Add(tmpEntity);
                    tmpEntity = null;
                }
                entity.ListEntity.AddRange(listEntity);
                //List<string> list = getInsertOrUpdateByExcuteClass(entity);
                //string sqlString = list.Aggregate((a, b) => a + "  " + b);
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                ExecuteNonQuery(entity);
                stopWatch.Stop();
                var time = stopWatch.ElapsedMilliseconds / 1000;
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ExecuteNonQuery<T>(ExcuteClass<T> entity)
        {
            DbCommand cmd = new SqlCommand();
            using (DbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = entity.ConnectionString;
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                int onceCount = 1000;
                bool isSuccess = true;
                for (var i = 0; i < entity.ListEntity.Count / onceCount + 1; i++)
                {
                    int surplusCount = entity.ListEntity.Count - i * onceCount > onceCount ? onceCount : entity.ListEntity.Count - i * onceCount;
                    if (surplusCount < 1)
                    {
                        break;
                    }
                    cmd.CommandText = getInsertOrUpdateByExcuteClass(entity, entity.ListEntity.Skip(i * onceCount).Take(surplusCount).ToList());
                    int val = cmd.ExecuteNonQuery();
                    if (val < 0)
                    {
                        isSuccess = false;
                    }
                }
                conn.Close();
                return isSuccess;
            }
        }

        private static string getInsertOrUpdateByExcuteClass<T>(ExcuteClass<T> entity, List<T> entityList)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();
            intitInsertOrUpdateByExcuteClass(entity, properties); 
            string tableName = entity.TableName;
            tableName = tableName ?? typeof(T).Name;
            string whereSql = "";
            string whereKeySql = "";
            string updateSql = "";
            string insertSql = "";
            string insertValueSql = "";
            foreach (var entityData in entityList)
            {
                whereSql = "";
                whereKeySql = "";
                updateSql = "";
                insertSql = "";
                insertValueSql = "";
                foreach (var whereColumn in entity.ListWhereColumns)
                {
                    whereSql += string.Format(@" {0} = '{1}' AND", whereColumn, properties.FirstOrDefault(p => p.Name == whereColumn).GetValue(entityData));
                    whereKeySql += string.Format(@" {0} = ''{1}'' AND", whereColumn, properties.FirstOrDefault(p => p.Name == whereColumn).GetValue(entityData));
                }
                whereSql = whereSql.Remove(whereSql.Length - 3);
                whereKeySql = whereKeySql.Remove(whereKeySql.Length - 3);
                sb.AppendFormat(@"
BEGIN TRY
IF EXISTS (SELECT 1 FROM {0} WHERE {1})", tableName, whereSql);
                foreach (var updateString in entity.ListUpdateColumns)
                {
                    updateSql += string.Format(@" {0} = '{1}',", updateString, properties.FirstOrDefault(p => p.Name == updateString).GetValue(entityData));
                }
                updateSql = updateSql.Remove(updateSql.Length - 1);
                sb.AppendFormat(@" 
    UPDATE {0} SET {2} WHERE {1}
ELSE ", tableName, whereSql, updateSql);
                foreach (var insertString in entity.ListInsertColumns)
                {
                    insertSql += string.Format(@"{0},", insertString);
                    insertValueSql += string.Format(@"'{0}',", properties.FirstOrDefault(p => p.Name == insertString).GetValue(entityData));
                }
                insertSql = insertSql.Remove(insertSql.Length - 1);
                insertValueSql = insertValueSql.Remove(insertValueSql.Length - 1);
                sb.AppendFormat(@"
    INSERT INTO {0}({1}) VALUES({2})
END TRY
BEGIN CATCH
	EXEC Pro_ErrorLog N'{3}',{4},'{5}','{6}'
END CATCH", tableName, insertSql, insertValueSql, whereKeySql, entity.AccountId, entity.MethodName, entity.Description);
            }
            return sb.ToString();
        }

        private static void intitInsertOrUpdateByExcuteClass<T>(ExcuteClass<T> entity, PropertyInfo[] properties)
        {
            List<string> insertList = new List<string>();
            List<string> updateList = new List<string>();
            if (entity.ListInsertColumns != null && entity.ListInsertColumns.Count > 0)
            {
                insertList = entity.ListInsertColumns;
            }
            if (insertList.Count == 0)
            {
                insertList = properties.Select(p => p.Name).ToList();
            }
            if (entity.ListNoInsertColumns != null && entity.ListNoInsertColumns.Count > 0)
            {
                insertList = insertList.Where(p => !entity.ListNoInsertColumns.Contains(p)).ToList();
            }
            entity.ListInsertColumns = insertList;
            if (entity.ListUpdateColumns != null && entity.ListUpdateColumns.Count > 0)
            {
                updateList = entity.ListUpdateColumns;
            }
            if (updateList.Count == 0)
            {
                updateList = properties.Select(p => p.Name).ToList();
            }
            if (entity.ListNoUpdateColumns != null && entity.ListNoUpdateColumns.Count > 0)
            {
                updateList = updateList.Where(p => !entity.ListNoUpdateColumns.Contains(p)).ToList();
            }
            entity.ListUpdateColumns = updateList;
        }
    }

    public class ExcuteClass<T>
    {
        public List<T> ListEntity { get; set; }

        public string ConnectionString { get; set; }

        public string TableName { get; set; }

        public List<string> ListInsertColumns { get; set; }

        public List<string> ListNoInsertColumns { get; set; }

        public List<string> ListUpdateColumns { get; set; }

        public List<string> ListNoUpdateColumns { get; set; }

        public List<string> ListWhereColumns { get; set; }

        public string MethodName { get; set; }

        public string Description { get; set; }

        public string AccountId { get; set; }

    }

    public class TestTmp
    {
        public string code { get; set; }

        public string value { get; set; }

        public DateTime? updateTime { get; set; }
    }
}