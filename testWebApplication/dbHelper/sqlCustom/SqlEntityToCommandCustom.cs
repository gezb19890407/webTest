using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Data;
using System.Linq;

namespace Sy6stem.Data
{
    /// <summary>
    /// 基于实体的数据库操作
    /// </summary>
    public class SqlEntityToCommandCustom : IEntityToCommandCustom
    {

        public ICommandCustom insert<T>(T entity)
        {
            if (entity != null)
            {
                SqlCommandCustom sqlCommand = new SqlCommandCustom(0);
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbPropertyName = new StringBuilder();
                StringBuilder sbPropertyValue = new StringBuilder();
                Type t = typeof(T);
                var properties = t.GetProperties();
                object objValue = null;
                foreach (PropertyInfo pi in properties)
                {
                    sbPropertyName.AppendFormat("{0},", pi.Name);
                    sbPropertyValue.AppendFormat("@{0},", pi.Name);
                    objValue = pi.GetValue(entity, null);
                    if (objValue != null)
                    {
                        sqlCommand.Parameters.AddWithValue(pi.Name, objValue);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue(pi.Name, DBNull.Value, pi.PropertyType);
                    }
                }
                sbPropertyName.Remove(sbPropertyName.Length - 1, 1);
                sbPropertyValue.Remove(sbPropertyValue.Length - 1, 1);
                sbSql.AppendFormat("insert into [{0}] ({1}) values ({2})", t.Name, sbPropertyName.ToString(), sbPropertyValue.ToString());
                sqlCommand.CommandText = sbSql.ToString();
                return sqlCommand;
            }
            else
            {
                throw new Exception("实例不能为null");
            }
        }

        public List<ICommandCustom> insert<T>(List<T> entityList)
        {
            if (entityList != null && entityList.Count > 0)
            {
                List<ICommandCustom> fwSqlCommandList = new List<ICommandCustom>();
                foreach (var entity in entityList)
                {
                    fwSqlCommandList.Add(insert<T>(entity));
                }
                return fwSqlCommandList;
            }
            else
            {
                throw new Exception("实例集合不能为空");
            }
        }

        //add by liuzhicheng
        public ICommandCustom updateExcludeNull<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            if (entity != null)
            {
                SqlCommandCustom sqlCommand = new SqlCommandCustom(-1);
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbPropertyNameValue = new StringBuilder();
                Type t = typeof(T);
                object objValue = null;
                var properties = t.GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    objValue = pi.GetValue(entity, null);
                    if (objValue != null)
                    {
                        sbPropertyNameValue.AppendFormat("{0}=@{0},", pi.Name);
                        sqlCommand.Parameters.AddWithValue(pi.Name, objValue);
                    }
                }
                sbPropertyNameValue.Remove(sbPropertyNameValue.Length - 1, 1);
                sbSql.AppendFormat("update [{0}] set {1} where {2}", t.Name, sbPropertyNameValue.ToString(), afterWhereSql);
                sqlCommand.CommandText = sbSql.ToString();
                if (afterWhereSqlParams != null)
                {
                    foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                    {
                        sqlCommand.Parameters.Add(fwParameter);
                    }
                }
                return sqlCommand;
            }
            else
            {
                throw new Exception("实例不能为null");
            }
        }

        public ICommandCustom update<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            if (entity != null)
            {
                SqlCommandCustom sqlCommand = new SqlCommandCustom(-1);
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbPropertyNameValue = new StringBuilder();
                Type t = typeof(T);
                object objValue = null;
                var properties = t.GetProperties();
                foreach (PropertyInfo pi in properties)
                {
                    objValue = pi.GetValue(entity, null);
                    if (objValue != null)
                    {
                        sbPropertyNameValue.AppendFormat("{0}=@{0},", pi.Name);
                        sqlCommand.Parameters.AddWithValue(pi.Name, objValue);
                    }
                }
                sbPropertyNameValue.Remove(sbPropertyNameValue.Length - 1, 1);
                sbSql.AppendFormat("update [{0}] set {1} where {2}", t.Name, sbPropertyNameValue.ToString(), afterWhereSql);
                sqlCommand.CommandText = sbSql.ToString();
                if (afterWhereSqlParams != null)
                {
                    foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                    {
                        sqlCommand.Parameters.Add(fwParameter);
                    }
                }
                return sqlCommand;
            }
            else
            {
                throw new Exception("实例不能为null");
            }
        }

        public List<ICommandCustom> update<T>(List<T> entityList, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            if (entityList != null && entityList.Count > 0)
            {
                List<ICommandCustom> fwSqlCommandList = new List<ICommandCustom>();
                foreach (var entity in entityList)
                {
                    fwSqlCommandList.Add(update<T>(entity, afterWhereSql, afterWhereSqlParams));
                }
                return fwSqlCommandList;
            }
            else
            {
                throw new Exception("实例集合不能为空");
            }
        }

        public List<ICommandCustom> insertOrUpdate<T>(List<T> entityList, List<string> idList, SqlDefaultPropertyName defaultPropertyName)
        {
            List<ICommandCustom> sqlCommandList = new List<ICommandCustom>();
            if (entityList != null && entityList.Count > 0 && idList != null && idList.Count > 0)
            {
                entityList = DataBaseCheckParam.checkParam<List<T>>(entityList);
                defaultPropertyName = DataBaseCheckParam.checkParam<SqlDefaultPropertyName>(defaultPropertyName);
                Type t = typeof(T);
                ICommandCustom cmd = new SqlCommandCustom();
                cmd.CommandText = string.Format(@"
SELECT {1}
FROM {0}
WHERE {1} IN ({2})", t.Name, defaultPropertyName.idPropertyName, // AND isnull({3},0) = 0
                    SqlCommandStaticHelper.joinToSqlString<string>(idList)); //defaultPropertyName.deletePropertyName
                DataTable dataTable = SqlCommandStaticHelper.ExecuteDataTable(cmd);
                PropertyInfo pi = null;
                object codeColumnValue = null;
                foreach (T entity in entityList)
                {
                    pi = t.GetProperty(defaultPropertyName.idPropertyName);
                    codeColumnValue = pi.GetValue(entity, null);
                    if (codeColumnValue == null)
                    {
                        codeColumnValue = Guid.NewGuid().ToString();
                        pi.SetValue(entity, codeColumnValue, null);
                    }
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        List<object> codeList = dataTable.AsEnumerable().Select(p => p.Field<object>(defaultPropertyName.idPropertyName)).ToList();
                        if (codeList.Contains(codeColumnValue))
                        {
                            defaultPropertyName.whetherUpdate = true;
                            setEntityData<T>(entity, defaultPropertyName);
                            sqlCommandList.Add(update<T>(entity, defaultPropertyName.idPropertyName + " = '" + codeColumnValue + "'", null)); //AND isnull(" + defaultPropertyName.deletePropertyName + ",0) = 0
                        }
                        else
                        {
                            defaultPropertyName.whetherUpdate = false;
                            setEntityData<T>(entity, defaultPropertyName);
                            sqlCommandList.Add(insert<T>(entity));
                        }
                    }
                    else
                    {
                        defaultPropertyName.whetherUpdate = false;
                        setEntityData<T>(entity, defaultPropertyName);
                        sqlCommandList.Add(insert<T>(entity));
                    }
                }
            }
            return sqlCommandList;
        }

        private void setEntityData<T>(T entity, SqlDefaultPropertyName defaultPropertyName)
        {
            PropertyInfo piCreaterId = null;
            PropertyInfo piCreateTime = null;
            PropertyInfo piUpdaterId = null;
            PropertyInfo piUpdateTime = null;
            PropertyInfo piDisState = null;
            DateTime dateTimeNow = DateTime.Now;
            Type t = typeof(T);
            piUpdaterId = t.GetProperty(defaultPropertyName.updaterId);
            piUpdateTime = t.GetProperty(defaultPropertyName.updateTime);
            piDisState = t.GetProperty(defaultPropertyName.deletePropertyName);
            if (piUpdaterId != null)
            {
                piUpdaterId.SetValue(entity, defaultPropertyName.userId, null);
            }
            if (piUpdateTime != null)
            {
                piUpdateTime.SetValue(entity, dateTimeNow, null);
            }
            if (piDisState != null)
            {
                if (piDisState.GetValue(entity, null) == null)
                {
                    piDisState.SetValue(entity, 0, null);
                }
            }
            if (!defaultPropertyName.whetherUpdate)
            {
                piCreaterId = t.GetProperty(defaultPropertyName.createrId);
                piCreateTime = t.GetProperty(defaultPropertyName.createTime);
                if (piCreaterId != null)
                {
                    piCreaterId.SetValue(entity, defaultPropertyName.userId, null);
                }
                if (piCreateTime != null)
                {
                    piCreateTime.SetValue(entity, dateTimeNow, null);
                }
            }
        }

        public IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(string.Empty, entity, idPropertyNameList, null);
        }

        public IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(connectionString, entity, idPropertyNameList, null);
        }

        public IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(connection, entity, null, idPropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(transaction, entity, idPropertyNameList, null);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(connectionString, entityList, idPropertyNameList, null);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(string.Empty, entityList, idPropertyNameList, null);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(connection, entityList, idPropertyNameList, null);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList)
        {
            return insertOrUpdate<T>(transaction, entityList, idPropertyNameList, null);
        }

        public IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return insertOrUpdate<T>(string.Empty, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return insertOrUpdate<T>(new ConnectionCustom(connectionString), entity, idPropertyNameList, uniquePropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            IDbResultCustom fwDBResult;
            if (connection == null)
            {
                fwDBResult = insertOrUpdate<T>(entity, idPropertyNameList, uniquePropertyNameList);
            }
            else
            {
                fwDBResult = new DbResultCustom();
                if (entity != null)
                {
                    SqlTransactionCustom transaction = new SqlTransactionCustom(connection.iDbConnection);
                    try
                    {
                        transaction.BeginTransaction();
                        fwDBResult = insertOrUpdate(transaction, entity, idPropertyNameList, uniquePropertyNameList);
                        transaction.Commit();
                        fwDBResult.dbResultStatus = EnumDbResultStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        fwDBResult.dbResultStatus = EnumDbResultStatus.Failure;
                        throw new Exception("执行insertOrUpdate事务出错！" + ex.ToString());
                    }

                }
                else
                {
                    fwDBResult.dbResultStatus = EnumDbResultStatus.Failure;
                    throw new Exception("实例集合不能为空");
                }
            }
            return fwDBResult;
        }

        public IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            IDbResultCustom fwDBResult;
            if (transaction == null)
            {
                fwDBResult = insertOrUpdate<T>(entity, idPropertyNameList, uniquePropertyNameList);
            }
            else
            {
                fwDBResult = new DbResultCustom();
                if (entity != null)
                {
                    Type t = typeof(T);
                    object objValue = null;
                    PropertyInfo pi = null;
                    var properties = t.GetProperties();
                    SqlCommandCustom cmdIDExist = new SqlCommandCustom();
                    StringBuilder sbIDProperty = new StringBuilder();
                    StringBuilder sbIDExist = new StringBuilder();
                    StringBuilder sbIDSelect = new StringBuilder();
                    if (idPropertyNameList != null && idPropertyNameList.Count > 0)
                    {
                        sbIDProperty.Append("(");
                        foreach (string idPropertyName in idPropertyNameList)
                        {
                            if (!string.IsNullOrEmpty(idPropertyName))
                            {
                                sbIDProperty.AppendFormat("{0}=@{0} AND ", idPropertyName);
                                sbIDSelect.AppendFormat("{0},", idPropertyName);
                            }
                        }
                        if (sbIDProperty.Length > 1)
                        {
                            sbIDProperty.Remove(sbIDProperty.Length - 5, 5);
                            sbIDProperty.Append(")");
                        }
                        else
                        {
                            //sbIDProperty.Clear();
                            sbIDProperty.Remove(0, sbIDProperty.Length);
                        }
                        if (sbIDSelect.Length > 0)
                        {
                            sbIDSelect.Remove(sbIDSelect.Length - 1, 1);
                        }
                    }
                    sbIDExist.AppendFormat(@"SELECT COUNT(*) FROM [{0}] WITH (NOLOCK) WHERE", t.Name);
                    if (sbIDProperty.Length > 0)
                    {
                        if (sbIDProperty.Length > 0)
                        {
                            sbIDExist.AppendFormat(@" {0}", sbIDProperty.ToString());
                        }
                        cmdIDExist.CommandText = sbIDExist.ToString();
                        cmdIDExist.Parameters.Clear();


                        foreach (string idPropertyName in idPropertyNameList)
                        {
                            pi = t.GetProperty(idPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                            if (pi != null)
                            {
                                cmdIDExist.Parameters.AddWithValue("@" + idPropertyName, pi.GetValue(entity, null));
                            }
                        }
                        int recordCount = SqlCommandStaticHelper.ExecuteScalar(transaction, cmdIDExist);


                        DataTable dtUnique = null;
                        //索引列查询
                        if (uniquePropertyNameList != null && uniquePropertyNameList.Count > 0)
                        {
                            bool isChangePropertyHasUniqueProperty = true;
                            StringBuilder sbNotChangeUniqueProperty = new StringBuilder();
                            foreach (string uniquePropertyName in uniquePropertyNameList)
                            {
                                //if (changePropertyDictionary.ContainsKey(uniquePropertyName))
                                //{
                                //    isChangePropertyHasUniqueProperty = true;
                                //}
                                //else
                                //{
                                //sbNotChangeUniqueProperty.AppendFormat(@"{0},", uniquePropertyName);
                                //}
                            }
                            //if (sbNotChangeUniqueProperty.Length > 1)
                            //{
                            //    sbNotChangeUniqueProperty.Remove(sbNotChangeUniqueProperty.Length - 1, 1);
                            //}
                            if (isChangePropertyHasUniqueProperty)
                            {
                                //使用索引列查询记录，用户判断唯一性
                                SqlCommandCustom cmdUniqueExist = new SqlCommandCustom();
                                StringBuilder sbUniqueProperty = new StringBuilder();
                                StringBuilder sbUniqueExist = new StringBuilder();

                                sbUniqueProperty.Append("(");
                                foreach (string uniquePropertyName in uniquePropertyNameList)
                                {
                                    if (!string.IsNullOrEmpty(uniquePropertyName))
                                    {
                                        sbUniqueProperty.AppendFormat("{0}=@{0} AND ", uniquePropertyName);
                                    }
                                }
                                if (sbUniqueProperty.Length > 1)
                                {
                                    sbUniqueProperty.Remove(sbUniqueProperty.Length - 5, 5);
                                    sbUniqueProperty.Append(")");
                                }
                                else
                                {
                                    //sbUniqueProperty.Clear();
                                    sbUniqueProperty.Remove(0, sbUniqueProperty.Length);
                                }
                                sbUniqueExist.AppendFormat(@"SELECT {0} FROM [{1}] WITH (NOLOCK) WHERE", sbIDSelect, t.Name);
                                if (sbUniqueProperty.Length > 0)
                                {
                                    if (sbUniqueProperty.Length > 0)
                                    {
                                        sbUniqueExist.AppendFormat(@" {0}", sbUniqueProperty.ToString());
                                    }
                                    cmdUniqueExist.CommandText = sbUniqueExist.ToString();
                                    cmdUniqueExist.Parameters.Clear();
                                    foreach (string uniquePropertyName in uniquePropertyNameList)
                                    {
                                        pi = t.GetProperty(uniquePropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                                        if (pi != null)
                                        {
                                            cmdUniqueExist.Parameters.AddWithValue("@" + uniquePropertyName, pi.GetValue(entity, null));
                                        }
                                    }
                                    dtUnique = SqlCommandStaticHelper.ExecuteDataTable(transaction, cmdUniqueExist);
                                }
                            }
                            else
                            {
                                if (recordCount > 0)
                                {
                                    //修改记录并且索引列的值没有赋值时 不需要验证索引列是否重复 可直接修改其他属性
                                }
                                else
                                {
                                    throw new Exception(string.Format("新增记录中索引列({0})没有赋值", sbNotChangeUniqueProperty.ToString()));
                                }
                            }
                        }
                        if (recordCount > 0)
                        {
                            //修改
                            bool isCanUpdate = true;
                            if (dtUnique != null && dtUnique.Rows.Count > 0)
                            {
                                foreach (string idPropertyName in idPropertyNameList)
                                {
                                    if (isCanUpdate)
                                    {
                                        if (!string.IsNullOrEmpty(idPropertyName))
                                        {
                                            pi = t.GetProperty(idPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                                            if (pi != null)
                                            {
                                                if (dtUnique.Rows[0][idPropertyName].ToString() != pi.GetValue(entity, null).ToString())
                                                {
                                                    isCanUpdate = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (isCanUpdate)
                            {
                                SqlCommandCustom cmdUpdate = new SqlCommandCustom(-1);
                                StringBuilder sbUpdate = new StringBuilder();
                                StringBuilder sbPropertyNameValue = new StringBuilder();
                                foreach (PropertyInfo property in properties)
                                {
                                    objValue = property.GetValue(entity, null);
                                    if (objValue != null)
                                    {
                                        sbPropertyNameValue.AppendFormat("{0}=@{0},", property.Name);
                                        cmdUpdate.Parameters.AddWithValue(property.Name, objValue);
                                    }
                                }
                                //foreach (String propertyName in changePropertyDictionary.Keys)
                                //{
                                //    pi = t.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                                //    if (pi != null)
                                //    {
                                //        sbPropertyNameValue.AppendFormat("[{0}]=@{0},", propertyName);
                                //        objValue = pi.GetValue(entity, null);
                                //        if (objValue != null)
                                //        {
                                //            cmdUpdate.Parameters.AddWithValue(propertyName, objValue);
                                //        }
                                //        else
                                //        {
                                //            cmdUpdate.Parameters.AddWithValue(propertyName, DBNull.Value, pi.PropertyType);
                                //        }
                                //    }
                                //}
                                if (sbPropertyNameValue.Length > 0)
                                {
                                    sbPropertyNameValue.Remove(sbPropertyNameValue.Length - 1, 1);
                                }
                                if (sbPropertyNameValue.Length > 0)
                                {
                                    sbUpdate.AppendFormat("UPDATE [{0}] SET {1} WHERE {2}", t.Name, sbPropertyNameValue.ToString(), sbIDProperty.ToString());
                                    cmdUpdate.CommandText = sbUpdate.ToString();
                                }
                                fwDBResult.dbResultStatus = SqlCommandStaticHelper.ExecuteNonQuery(transaction, cmdUpdate) > -1 ? EnumDbResultStatus.Success : EnumDbResultStatus.Failure;
                            }
                            else
                            {
                                throw new Exception("索引列存在重复");
                            }
                            fwDBResult.dbAction = EnumDbAction.Update;
                        }
                        else
                        {
                            //新增
                            bool isCanInsert = true;
                            if (dtUnique != null && dtUnique.Rows.Count > 0)
                            {
                                isCanInsert = false;
                            }
                            if (isCanInsert)
                            {
                                SqlCommandCustom cmdInsert = new SqlCommandCustom(0);
                                StringBuilder sbInsert = new StringBuilder();
                                StringBuilder sbPropertyName = new StringBuilder();
                                StringBuilder sbPropertyValue = new StringBuilder();
                                foreach (PropertyInfo property in properties)
                                {
                                    objValue = property.GetValue(entity, null);
                                    if (objValue != null)
                                    {
                                        sbPropertyName.AppendFormat("[{0}],", property.Name);
                                        sbPropertyValue.AppendFormat("@{0},", property.Name);
                                        cmdInsert.Parameters.AddWithValue(property.Name, objValue);
                                    }
                                }
                                //foreach (String propertyName in changePropertyDictionary.Keys)
                                //{
                                //    pi = t.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                                //    if (pi != null)
                                //    {
                                //        sbPropertyName.AppendFormat("[{0}],", propertyName);
                                //        sbPropertyValue.AppendFormat("@{0},", propertyName);
                                //        objValue = pi.GetValue(entity, null);
                                //        if (objValue != null)
                                //        {
                                //            cmdInsert.Parameters.AddWithValue(propertyName, objValue);
                                //        }
                                //        else
                                //        {
                                //            cmdInsert.Parameters.AddWithValue(propertyName, DBNull.Value, pi.PropertyType);
                                //        }
                                //    }
                                //}
                                if (sbPropertyName.Length > 0)
                                {
                                    sbPropertyName.Remove(sbPropertyName.Length - 1, 1);
                                }
                                if (sbPropertyValue.Length > 0)
                                {
                                    sbPropertyValue.Remove(sbPropertyValue.Length - 1, 1);
                                }
                                if (sbPropertyName.Length > 0 && sbPropertyValue.Length > 0)
                                {
                                    sbInsert.AppendFormat("INSERT INTO [{0}] ({1}) VALUES ({2})", t.Name, sbPropertyName.ToString(), sbPropertyValue.ToString());

                                    cmdInsert.CommandText = sbInsert.ToString();
                                }
                                fwDBResult.dbResultStatus = SqlCommandStaticHelper.ExecuteNonQuery(transaction, cmdInsert) > 0 ? EnumDbResultStatus.Success : EnumDbResultStatus.Failure;
                            }
                            else
                            {
                                //undo
                                throw new Exception("索引列存在重复");
                            }
                            fwDBResult.dbAction = EnumDbAction.Insert;
                        }
                    }
                    else
                    {
                        throw new Exception("请指示ID字段的集合（idPropertyNameList）");
                    }
                }
                else
                {
                    throw new Exception("实例不能为null");
                }
            }
            return fwDBResult;
        }

        public List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return insertOrUpdate<T>(string.Empty, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return insertOrUpdate<T>(new ConnectionCustom(connectionString), entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            List<IDbResultCustom> fwDBResultList;
            if (connection == null)
            {
                fwDBResultList = insertOrUpdate<T>(entityList, idPropertyNameList, uniquePropertyNameList);
            }
            else
            {
                fwDBResultList = new List<IDbResultCustom>();
                if (entityList != null && entityList.Count > 0)
                {
                    SqlTransactionCustom transaction = new SqlTransactionCustom(connection.iDbConnection);
                    try
                    {
                        transaction.BeginTransaction();
                        fwDBResultList = insertOrUpdate(transaction, entityList, idPropertyNameList, uniquePropertyNameList);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("执行insertOrUpdate事务出错！" + ex.ToString());
                    }

                }
                else
                {
                    throw new Exception("实例集合不能为空");
                }
            }
            return fwDBResultList;
        }

        public List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            List<IDbResultCustom> fwDBResultList;
            if (transaction == null)
            {
                fwDBResultList = insertOrUpdate<T>(entityList, idPropertyNameList, uniquePropertyNameList);
            }
            else
            {
                fwDBResultList = new List<IDbResultCustom>();
                if (entityList != null && entityList.Count > 0)
                {
                    foreach (var entity in entityList)
                    {
                        fwDBResultList.Add(insertOrUpdate<T>(transaction, entity, idPropertyNameList, uniquePropertyNameList));
                    }
                }
                else
                {
                    throw new Exception("实例集合不能为空");
                }
            }
            return fwDBResultList;
        }

        public List<ICommandCustom> delete(string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return delete(string.Empty, tableName, afterWhereSql, afterWhereSqlParams); ;
        }

        public List<ICommandCustom> delete(string connectionString, string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return delete(new ConnectionCustom(connectionString), tableName, afterWhereSql, afterWhereSqlParams);
        }

        public List<ICommandCustom> delete(IConnectionCustom connection, string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            List<ICommandCustom> fwSqlCommandList = new List<ICommandCustom>();
            SqlCommandCustom sqlCommand = new SqlCommandCustom();
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbSearchField = new StringBuilder();
            sbSql.AppendFormat(@"
SELECT
	OBJECT_NAME(con.constid) foreignKeyRelationName,
	OBJECT_NAME(sf.fkeyid) id,
	fcol.name columnName,
	OBJECT_NAME(sf.rkeyid) pid,
	rcol.name foreignKeyColumnName,
	st.name dataType
FROM
	sysforeignkeys sf
	INNER JOIN sysconstraints con ON sf.constid = con.constid
	INNER JOIN sys.syscolumns fcol ON fcol.id = sf.fkeyid AND fcol.colid = sf.fkey
	INNER JOIN sys.syscolumns rcol ON rcol.id = sf.rkeyid AND rcol.colid = sf.rkey
	INNER JOIN sys.systypes st ON fcol.type = st.type and fcol.xusertype=st.xusertype
where
    OBJECT_NAME(sf.rkeyid)=@tableName
            ");
            sqlCommand.CommandText = sbSql.ToString();
            sqlCommand.Parameters.AddWithValue("@tableName", tableName);
            DataTable dtFK = SqlCommandStaticHelper.ExecuteDataTable(connection, sqlCommand);
            if (dtFK != null && dtFK.Rows.Count > 0)
            {
                //DataTable dtSearchField = dtFK.DefaultView.ToTable(dtFK.TableName, true, new string[1] { "foreignKeyColumnName" });
                //foreach (DataRow dr in dtSearchField.Rows)
                //{
                //    sbSearchField.AppendFormat("[{0}],", dr["foreignKeyColumnName"].ToString());
                //}
                //sbSearchField.Remove(sbSearchField.Length - 1, 1);

                //sqlCommand = new SqlCommandCustom();
                //sbSql = new StringBuilder();
                //sbSql.AppendFormat("select {0} from [{1}] where {2}", sbSearchField.ToString(), tableName, afterWhereSql);
                //sqlCommand.CommandText = sbSql.ToString();
                //if (afterWhereSqlParams != null)
                //{
                //    foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                //    {
                //        sqlCommand.Parameters.Add(fwParameter);
                //    }
                //}
                //DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(transaction, sqlCommand);
                foreach (DataRow dr in dtFK.Rows)
                {
                    fwSqlCommandList.AddRange(delete(connection, dr["id"].ToString(), "[" + dr["columnName"].ToString() + "] in (select [" + dr["columnName"].ToString() + "] from [" + dr["pid"].ToString() + "] where " + afterWhereSql + ")", afterWhereSqlParams));
                }
            }
            sqlCommand = new SqlCommandCustom(-1);
            sbSql = new StringBuilder();
            sbSql.AppendFormat("delete from [{0}] where {1}", tableName, afterWhereSql);
            sqlCommand.CommandText = sbSql.ToString();
            if (afterWhereSqlParams != null)
            {
                foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                {
                    sqlCommand.Parameters.Add(fwParameter);
                }
            }
            fwSqlCommandList.Add(sqlCommand);
            return fwSqlCommandList;
        }

        public List<ICommandCustom> delete(ITransactionCustom transaction, string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            List<ICommandCustom> fwSqlCommandList = new List<ICommandCustom>();
            SqlCommandCustom sqlCommand = new SqlCommandCustom();
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbSearchField = new StringBuilder();
            sbSql.AppendFormat(@"
SELECT
	OBJECT_NAME(con.constid) foreignKeyRelationName,
	OBJECT_NAME(sf.fkeyid) id,
	fcol.name columnName,
	OBJECT_NAME(sf.rkeyid) pid,
	rcol.name foreignKeyColumnName,
	st.name dataType
FROM
	sysforeignkeys sf
	INNER JOIN sysconstraints con ON sf.constid = con.constid
	INNER JOIN sys.syscolumns fcol ON fcol.id = sf.fkeyid AND fcol.colid = sf.fkey
	INNER JOIN sys.syscolumns rcol ON rcol.id = sf.rkeyid AND rcol.colid = sf.rkey
	INNER JOIN sys.systypes st ON fcol.type = st.type and fcol.xusertype=st.xusertype
where
    OBJECT_NAME(sf.rkeyid)=@tableName
            ");
            sqlCommand.CommandText = sbSql.ToString();
            sqlCommand.Parameters.AddWithValue("@tableName", tableName);
            DataTable dtFK = SqlCommandStaticHelper.ExecuteDataTable(transaction, sqlCommand);
            if (dtFK != null && dtFK.Rows.Count > 0)
            {
                //DataTable dtSearchField = dtFK.DefaultView.ToTable(dtFK.TableName, true, new string[1] { "foreignKeyColumnName" });
                //foreach (DataRow dr in dtSearchField.Rows)
                //{
                //    sbSearchField.AppendFormat("[{0}],", dr["foreignKeyColumnName"].ToString());
                //}
                //sbSearchField.Remove(sbSearchField.Length - 1, 1);

                //sqlCommand = new SqlCommandCustom();
                //sbSql = new StringBuilder();
                //sbSql.AppendFormat("select {0} from [{1}] where {2}", sbSearchField.ToString(), tableName, afterWhereSql);
                //sqlCommand.CommandText = sbSql.ToString();
                //if (afterWhereSqlParams != null)
                //{
                //    foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                //    {
                //        sqlCommand.Parameters.Add(fwParameter);
                //    }
                //}
                //DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(transaction, sqlCommand);
                foreach (DataRow dr in dtFK.Rows)
                {
                    fwSqlCommandList.AddRange(delete(transaction, dr["id"].ToString(), "[" + dr["columnName"].ToString() + "] in (select [" + dr["columnName"].ToString() + "] from [" + dr["pid"].ToString() + "] where " + afterWhereSql + ")", afterWhereSqlParams));
                }
            }
            sqlCommand = new SqlCommandCustom(-1);
            sbSql = new StringBuilder();
            sbSql.AppendFormat("delete from [{0}] where {1}", tableName, afterWhereSql);
            sqlCommand.CommandText = sbSql.ToString();
            if (afterWhereSqlParams != null)
            {
                foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                {
                    sqlCommand.Parameters.Add(fwParameter);
                }
            }
            fwSqlCommandList.Add(sqlCommand);
            return fwSqlCommandList;
        }

        public List<ICommandCustom> delete<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return delete(typeof(T).Name, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return query<T>(string.Empty, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(string connectionString, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return query<T>(new ConnectionCustom(connectionString), afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(IConnectionCustom connection, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            Type t = typeof(T);
            SqlCommandCustom sqlCommand = new SqlCommandCustom();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select top 1 * from {0} where {1}", t.Name, afterWhereSql);
            sqlCommand.CommandText = sbSql.ToString();
            if (afterWhereSqlParams != null)
            {
                foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                {
                    sqlCommand.Parameters.Add(fwParameter);
                }
            }
            return query<T>(connection, sqlCommand);
        }

        public T query<T>(ITransactionCustom transaction, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            Type t = typeof(T);
            SqlCommandCustom sqlCommand = new SqlCommandCustom();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select top 1 * from {0} where {1}", t.Name, afterWhereSql);
            sqlCommand.CommandText = sbSql.ToString();
            if (afterWhereSqlParams != null)
            {
                foreach (ParameterCustom fwParameter in afterWhereSqlParams)
                {
                    sqlCommand.Parameters.Add(fwParameter);
                }
            }
            return query<T>(transaction, sqlCommand);
        }

        public T query<T>(ICommandCustom fwcmd)
        {
            return query<T>(string.Empty, fwcmd);
        }

        public T query<T>(string connectionString, ICommandCustom fwcmd)
        {
            return query<T>(new ConnectionCustom(connectionString), fwcmd);
        }

        public T query<T>(IConnectionCustom connection, ICommandCustom fwcmd)
        {
            T entity;
            if (connection == null)
            {
                entity = query<T>(fwcmd);
            }
            else
            {
                DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(connection, fwcmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfo[] PropertyInfoArray = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoArray)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            Object ObjectValue = dt.Rows[0][pi.Name];
                            if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(entity, ObjectValue, null);
                            }
                        }
                    }
                }
                else
                {
                    entity = default(T);
                }
            }
            return entity;
        }

        public T query<T>(ITransactionCustom transaction, ICommandCustom fwcmd)
        {
            T entity;
            if (transaction == null)
            {
                entity = query<T>(fwcmd);
            }
            else
            {
                DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(transaction, fwcmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    entity = (T)Activator.CreateInstance(typeof(T));
                    Type t = typeof(T);
                    PropertyInfo[] PropertyInfoArray = t.GetProperties();
                    foreach (PropertyInfo pi in PropertyInfoArray)
                    {
                        if (dt.Columns.Contains(pi.Name))
                        {
                            Object ObjectValue = dt.Rows[0][pi.Name];
                            if (ObjectValue != DBNull.Value)
                            {
                                ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                pi.SetValue(entity, ObjectValue, null);
                            }
                        }
                    }
                }
                else
                {
                    entity = default(T);
                }
            }
            return entity;
        }

        public List<T> queryList<T>(ICommandCustom fwcmd)
        {
            return queryList<T>(string.Empty, fwcmd);
        }

        public List<T> queryList<T>(string connectionString, ICommandCustom fwcmd)
        {
            return queryList<T>(new ConnectionCustom(connectionString), fwcmd);
        }

        public List<T> queryList<T>(IConnectionCustom connection, ICommandCustom fwcmd)
        {
            List<T> entityList;
            if (connection == null)
            {
                entityList = queryList<T>(fwcmd);
            }
            else
            {
                entityList = new List<T>();
                DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(connection, fwcmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type t = typeof(T);
                    T entity;
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {
                        entity = (T)Activator.CreateInstance(typeof(T));
                        PropertyInfo[] PropertyInfoArray = t.GetProperties();
                        foreach (PropertyInfo pi in PropertyInfoArray)
                        {
                            if (dt.Columns.Contains(pi.Name))
                            {
                                Object ObjectValue = dt.Rows[i][pi.Name];
                                if (ObjectValue != DBNull.Value)
                                {
                                    ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                    pi.SetValue(entity, ObjectValue, null);
                                }
                            }
                        }
                        entityList.Add(entity);
                    }

                }
            }
            return entityList;
        }

        public List<T> queryList<T>(ITransactionCustom transaction, ICommandCustom fwcmd)
        {
            List<T> entityList;
            if (transaction == null)
            {
                entityList = queryList<T>(fwcmd);
            }
            else
            {
                entityList = new List<T>();
                DataTable dt = SqlCommandStaticHelper.ExecuteDataTable(transaction, fwcmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Type t = typeof(T);
                    T entity;
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {
                        entity = (T)Activator.CreateInstance(typeof(T));
                        PropertyInfo[] PropertyInfoArray = t.GetProperties();
                        foreach (PropertyInfo pi in PropertyInfoArray)
                        {
                            if (dt.Columns.Contains(pi.Name))
                            {
                                Object ObjectValue = dt.Rows[i][pi.Name];
                                if (ObjectValue != DBNull.Value)
                                {
                                    ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
                                    pi.SetValue(entity, ObjectValue, null);
                                }
                            }
                        }
                        entityList.Add(entity);
                    }
                }
            }
            return entityList;
        }

        //public FWPageData<T> queryPage<T>(IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return queryPage<T>(string.Empty, fwPageProcedureParams);
        //}

        //public FWPageData<T> queryPage<T>(string connectionString, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return queryPage<T>(new ConnectionCustom(connectionString), fwPageProcedureParams);
        //}

        //public FWPageData<T> queryPage<T>(IConnectionCustom connection, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    FWPageData<T> fwPageData;
        //    if (connection == null)
        //    {
        //        fwPageData = queryPage<T>(fwPageProcedureParams);
        //    }
        //    else
        //    {
        //        fwPageData = new FWPageData<T>();
        //        Int64 RecordCount = 0;
        //        Int64 PageSize = 0;
        //        Int64 PageNow = 0;
        //        FWSqlPageProcedureHelper fwSqlPageProcedureHelper = new FWSqlPageProcedureHelper();
        //        DataTable dt = fwSqlPageProcedureHelper.PageProcedureDataTable(connection, ref RecordCount, ref PageSize, ref PageNow, fwPageProcedureParams);
        //        fwPageData.recordCount = RecordCount;
        //        fwPageData.pageSize = PageSize;
        //        fwPageData.pageIndex = PageNow;
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            for (Int32 i = 0; i < dt.Rows.Count; i++)
        //            {
        //                T Entity = (T)Activator.CreateInstance(typeof(T));
        //                Type t = typeof(T);
        //                PropertyInfo[] PropertyInfoS = t.GetProperties();
        //                foreach (PropertyInfo pi in PropertyInfoS)
        //                {
        //                    if (dt.Columns.Contains(pi.Name))
        //                    {
        //                        Object ObjectValue = dt.Rows[i][pi.Name];
        //                        if (ObjectValue != DBNull.Value)
        //                        {
        //                            ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
        //                            pi.SetValue(Entity, ObjectValue, null);
        //                        }
        //                    }
        //                }
        //                fwPageData.entityList.Add(Entity);
        //            }
        //        }
        //    }
        //    return fwPageData;
        //}

        //public FWPageData<T> queryPage<T>(ITransactionCustom transaction, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    FWPageData<T> fwPageData;
        //    if (transaction == null)
        //    {
        //        fwPageData = queryPage<T>(fwPageProcedureParams);
        //    }
        //    else
        //    {
        //        fwPageData = new FWPageData<T>();
        //        Int64 RecordCount = 0;
        //        Int64 PageSize = 0;
        //        Int64 PageNow = 0;
        //        FWSqlPageProcedureHelper fwSqlPageProcedureHelper = new FWSqlPageProcedureHelper();
        //        DataTable dt = fwSqlPageProcedureHelper.PageProcedureDataTable(transaction, ref RecordCount, ref PageSize, ref PageNow, fwPageProcedureParams);
        //        fwPageData.recordCount = RecordCount;
        //        fwPageData.pageSize = PageSize;
        //        fwPageData.pageIndex = PageNow;
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            for (Int32 i = 0; i < dt.Rows.Count; i++)
        //            {
        //                T Entity = (T)Activator.CreateInstance(typeof(T));
        //                Type t = typeof(T);
        //                PropertyInfo[] PropertyInfoS = t.GetProperties();
        //                foreach (PropertyInfo pi in PropertyInfoS)
        //                {
        //                    if (dt.Columns.Contains(pi.Name))
        //                    {
        //                        Object ObjectValue = dt.Rows[i][pi.Name];
        //                        if (ObjectValue != DBNull.Value)
        //                        {
        //                            ObjectValue = TypeConvert(ObjectValue, pi.PropertyType);
        //                            pi.SetValue(Entity, ObjectValue, null);
        //                        }
        //                    }
        //                }
        //                fwPageData.entityList.Add(Entity);
        //            }
        //        }
        //    }
        //    return fwPageData;
        //}



        /// <summary>
        /// 分页存储过程
        /// </summary>
        /// <typeparam name="T">要分页查询的对象</typeparam>
        /// <param name="_PageProcedureParameter">分页查询的参数</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>


        public static Object TypeConvert(Object ObjectValue, Type _Type)
        {
            if (ObjectValue != null)
            {
                Type ObjectValueType = ObjectValue.GetType();
                if (ObjectValueType != _Type)
                {
                    if (ObjectValueType == typeof(Boolean) && (_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    {
                        if (Convert.ToBoolean(ObjectValue))
                        {
                            ObjectValue = 1;
                        }
                        else
                        {
                            ObjectValue = 0;
                        }
                    }
                    //if (ObjectValueType == typeof(Int16) && (_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    //{
                    //    ObjectValue = Convert.ToInt32(ObjectValue);
                    //}
                    if ((_Type == typeof(Int32) || _Type == typeof(Int32?)))
                    {
                        ObjectValue = Convert.ToInt32(ObjectValue);
                    }
                    if (ObjectValueType == typeof(Decimal) && (_Type == typeof(Double) || _Type == typeof(Double?)))
                    {
                        ObjectValue = Convert.ToDouble(ObjectValue);
                    }
                }
            }
            return ObjectValue;
        }

        public static T ConvertTo<T>(Object _Object, Dictionary<String, String> PropertyNameMapping)
        {
            Type tEntity = typeof(T);
            T Entity = (T)Activator.CreateInstance(typeof(T));

            Type tObject = _Object.GetType();
            HashSet<String> EntityToSqlParameters = (HashSet<String>)tObject.GetField("EntityToSqlParameters", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_Object);
            if (EntityToSqlParameters != null && EntityToSqlParameters.Count > 0)
            {
                foreach (String Key in EntityToSqlParameters)
                {
                    PropertyInfo pi_Object = tObject.GetProperty(Key, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    if (pi_Object != null && PropertyNameMapping.ContainsKey(Key))
                    {
                        PropertyInfo pi_Entity = tEntity.GetProperty(PropertyNameMapping[Key], BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                        if (pi_Entity != null)
                        {
                            Object ObjectValue = pi_Object.GetValue(_Object, null);
                            if (ObjectValue != null)
                            {
                                Type ObjectValueType = ObjectValue.GetType();
                                Type _Type = pi_Entity.PropertyType;
                                if (ObjectValueType != _Type)
                                {
                                    if ((_Type == typeof(Decimal) || _Type == typeof(Decimal?)))
                                    {
                                        ObjectValue = Convert.ToDecimal(ObjectValue);
                                    }
                                    if ((_Type == typeof(Int32) || _Type == typeof(Int32?)))
                                    {
                                        ObjectValue = Convert.ToInt32(ObjectValue);
                                    }
                                }
                                //if (pi_Object.PropertyType.FullName == "System.Int32" && pi_Entity.PropertyType.FullName == "System.Boolean")
                                //{
                                //    pi_Entity.SetValue(Entity, pi_Object.GetValue(_Object, null).ToString() == "1" ? true : false, null);
                                //}
                            }
                            pi_Entity.SetValue(Entity, ObjectValue, null);

                        }
                    }
                }
            }
            return Entity;
        }
    }
}
