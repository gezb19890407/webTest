using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// 基于FWDataTable对象的数据库操作
    /// </summary>
    public interface IEntityToCommandCustom
    {
        ICommandCustom insert<T>(T entity);

        List<ICommandCustom> insert<T>(List<T> entityList);


        ICommandCustom update<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        ICommandCustom updateExcludeNull<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        List<ICommandCustom> update<T>(List<T> entityList, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);


        IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList);

        IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList);

        IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList);

        IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList);


        List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList);


        IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList);


        List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList);

        List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList);


        List<ICommandCustom> delete(string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        List<ICommandCustom> delete<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);


        T query<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        T query<T>(string connectionString, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        T query<T>(IConnectionCustom connection, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);

        T query<T>(ITransactionCustom transaction, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams);


        T query<T>(ICommandCustom fwcmd);

        T query<T>(string connectionString, ICommandCustom fwcmd);

        T query<T>(IConnectionCustom connection, ICommandCustom fwcmd);

        T query<T>(ITransactionCustom transaction, ICommandCustom fwcmd);


        List<T> queryList<T>(ICommandCustom fwcmd);

        List<T> queryList<T>(string connectionString, ICommandCustom fwcmd);

        List<T> queryList<T>(IConnectionCustom connection, ICommandCustom fwcmd);

        List<T> queryList<T>(ITransactionCustom transaction, ICommandCustom fwcmd);


        //FWPageData<T> queryPage<T>(IFWPageProcedureParams fwPageProcedureParams);

        //FWPageData<T> queryPage<T>(string connectionString, IFWPageProcedureParams fwPageProcedureParams);

        //FWPageData<T> queryPage<T>(IConnectionCustom connection, IFWPageProcedureParams fwPageProcedureParams);

        //FWPageData<T> queryPage<T>(ITransactionCustom transaction, IFWPageProcedureParams fwPageProcedureParams);


    }
}