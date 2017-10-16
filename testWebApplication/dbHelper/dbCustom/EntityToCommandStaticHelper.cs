using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    public class EntityToCommandStaticHelper
    {
        public static ICommandCustom insert<T>(T entity)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insert<T>(entity);
        }

        public static List<ICommandCustom> insert<T>(List<T> entityList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insert<T>(entityList);
        }

        //add by liuzhicheng
        public static ICommandCustom updateExcludeNull<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.updateExcludeNull<T>(entity, afterWhereSql, afterWhereSqlParams);
        }

        public static ICommandCustom update<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.update<T>(entity, afterWhereSql, afterWhereSqlParams);
        }

        public static List<ICommandCustom> update<T>(List<T> entityList, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.update<T>(entityList, afterWhereSql, afterWhereSqlParams);
        }

        public static IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insertOrUpdate<T>(entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
            return fwEntityToFWCommand.insertOrUpdate<T>(connectionString, entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
            return fwEntityToFWCommand.insertOrUpdate<T>(connection, entity, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
            return fwEntityToFWCommand.insertOrUpdate<T>(transaction, entity, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
            return fwEntityToFWCommand.insertOrUpdate<T>(connectionString, entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insertOrUpdate<T>(entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
            return fwEntityToFWCommand.insertOrUpdate<T>(connection, entityList, idPropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
            return fwEntityToFWCommand.insertOrUpdate<T>(transaction, entityList, idPropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insertOrUpdate<T>(entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
            return fwEntityToFWCommand.insertOrUpdate<T>(connectionString, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
            return fwEntityToFWCommand.insertOrUpdate<T>(connection, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
            return fwEntityToFWCommand.insertOrUpdate<T>(transaction, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.insertOrUpdate<T>(entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
            return fwEntityToFWCommand.insertOrUpdate<T>(connectionString, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
            return fwEntityToFWCommand.insertOrUpdate<T>(connection, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
            return fwEntityToFWCommand.insertOrUpdate<T>(transaction, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public static List<ICommandCustom> delete(string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.delete(tableName, afterWhereSql, afterWhereSqlParams);
        }

        public static List<ICommandCustom> delete<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.delete<T>(afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
            return fwEntityToFWCommand.query<T>(afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(string connectionString, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
            return fwEntityToFWCommand.query<T>(connectionString, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(IConnectionCustom connection, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
            return fwEntityToFWCommand.query<T>(connection, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(ITransactionCustom transaction, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
            return fwEntityToFWCommand.query<T>(transaction, afterWhereSql, afterWhereSqlParams);
        }

        public static T query<T>(ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.query<T>(command);
        }

        public static T query<T>(string connectionString, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.query<T>(connectionString, command);
        }

        public static T query<T>(IConnectionCustom connection, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.query<T>(connection, command);
        }

        public static T query<T>(ITransactionCustom transaction, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.query<T>(transaction, command);
        }

        public static List<T> queryList<T>(ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.queryList<T>(command);
        }

        public static List<T> queryList<T>(string connectionString, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.queryList<T>(connectionString, command);
        }

        public static List<T> queryList<T>(IConnectionCustom connection, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.queryList<T>(connection, command);
        }

        public static List<T> queryList<T>(ITransactionCustom transaction, ICommandCustom command)
        {
            EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(command.DatabaseTypeCustom);
            return fwEntityToFWCommand.queryList<T>(transaction, command);
        }

        //public static FWPageData<T> queryPage<T>(IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom();
        //    return fwEntityToFWCommand.queryPage<T>(fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(string connectionString, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connectionString);
        //    return fwEntityToFWCommand.queryPage<T>(connectionString, fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(IConnectionCustom connection, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(connection);
        //    return fwEntityToFWCommand.queryPage<T>(connection, fwPageProcedureParams);
        //}

        //public static FWPageData<T> queryPage<T>(ITransactionCustom transaction, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    EntityToCommandCustom fwEntityToFWCommand = new EntityToCommandCustom(transaction);
        //    return fwEntityToFWCommand.queryPage<T>(transaction, fwPageProcedureParams);
        //}
    }
}
