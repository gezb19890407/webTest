using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sy6stem.Data
{
    public class SqlEntityToCommandStaticHelper
    {
        public static ICommandCustom insert<T>(T entity)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insert<T>(entity);
        }

        public static List<ICommandCustom> insert<T>(List<T> entityList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insert<T>(entityList);
        }

        public static ICommandCustom update<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.update<T>(entity, afterWhereSql, afterWhereSqlParams);
        }

        public static List<ICommandCustom> update<T>(List<T> entityList, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.update<T>(entityList, afterWhereSql, afterWhereSqlParams);
        }

        public static IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connectionString, entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connection, entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(transaction, entity, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connectionString, entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connection, entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(transaction, entityList, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connectionString, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connection, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(transaction, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connectionString, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(connection, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.insertOrUpdate<T>(transaction, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<ICommandCustom> delete(string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.delete(tableName, afterWhereSql, afterWhereSqlParams);
        }

        public static List<ICommandCustom> delete<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.delete<T>(afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(string connectionString, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(connectionString, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(IConnectionCustom connection, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(connection, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(ITransactionCustom transaction, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(transaction, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(command);
        }

        public static T query<T>(string connectionString, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(connectionString, command);
        }

        public static T query<T>(IConnectionCustom connection, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(connection, command);
        }

        public static T query<T>(ITransactionCustom transaction, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.query<T>(transaction, command);
        }

        public static List<T> queryList<T>(ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.queryList<T>(command);
        }

        public static List<T> queryList<T>(string connectionString, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.queryList<T>(connectionString, command);
        }

        public static List<T> queryList<T>(IConnectionCustom connection, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.queryList<T>(connection, command);
        }

        public static List<T> queryList<T>(ITransactionCustom transaction, ICommandCustom command)
        {
            SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
            return sqlEntityToCommandCustom.queryList<T>(transaction, command);
        }

        //public static FWPageData<T> queryPage<T>(IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
        //    return sqlEntityToCommandCustom.queryPage<T>(fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(string connectionString, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
        //    return sqlEntityToCommandCustom.queryPage<T>(connectionString, fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(IConnectionCustom connection, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
        //    return sqlEntityToCommandCustom.queryPage<T>(connection, fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(ITransactionCustom transaction, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    SqlEntityToCommandCustom sqlEntityToCommandCustom = new SqlEntityToCommandCustom();
        //    return sqlEntityToCommandCustom.queryPage<T>(transaction, fwPageProcedureParams);
        //}
    }
}
