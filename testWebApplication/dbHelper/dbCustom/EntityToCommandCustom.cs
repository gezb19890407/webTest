using Sy6stem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    public class EntityToCommandCustom : IEntityToCommandCustom
    {
        public DatabaseTypeCustom DatabaseTypeCustom
        {
            get { return _fwDatabaseType; }
        }
        private DatabaseTypeCustom _fwDatabaseType;

        public IEntityToCommandCustom entityToCommandCustom;

        public EntityToCommandCustom(DatabaseTypeCustom DatabaseTypeCustom)
        {
            _fwDatabaseType = DatabaseTypeCustom;
            switch (_fwDatabaseType)
            {
                case DatabaseTypeCustom.Sql:
                    entityToCommandCustom = new SqlEntityToCommandCustom();
                    break;
                case DatabaseTypeCustom.Oracle:
                    //iFWCommand = new FWOracleCommand(recordCount, fwComparisonType);
                    break;
                case DatabaseTypeCustom.Odbc:
                    //entityToCommandCustom = new FWOdbcCommand(recordCount, fwComparisonType);
                    break;
                case DatabaseTypeCustom.OleDb:
                    //_iFWCommand = new FWOleDbCommand(recordCount, fwComparisonType);
                    break;
                case DatabaseTypeCustom.MySql:
                    //entityToCommandCustom = new FWMySqlEntityToFWCommand();
                    break;
            }
        }

        public EntityToCommandCustom(ITransactionCustom fwTransaction)
            : this(fwTransaction != null ? fwTransaction.DatabaseTypeCustom : (new ConnectionCustom()).DatabaseTypeCustom)
        {
        }

        public EntityToCommandCustom(IConnectionCustom fwConnection)
            : this(fwConnection != null ? fwConnection.DatabaseTypeCustom : (new ConnectionCustom()).DatabaseTypeCustom)
        {
        }

        public EntityToCommandCustom(string connectionString)
            : this(new ConnectionCustom(connectionString))
        {
        }

        public EntityToCommandCustom()
            : this(string.Empty)
        {
        }


        public ICommandCustom insert<T>(T entity)
        {
            return entityToCommandCustom.insert(entity);
        }

        public List<ICommandCustom> insert<T>(List<T> entityList)
        {
            return entityToCommandCustom.insert(entityList);
        }

        public ICommandCustom update<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.update(entity, afterWhereSql, afterWhereSqlParams);
        }

        public List<ICommandCustom> update<T>(List<T> entityList, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.update(entityList, afterWhereSql, afterWhereSqlParams);
        }

        public IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(ConnectionCustom.CONNECTION_STRING, entity, idPropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connectionString, entity, idPropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connection, entity, idPropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(transaction, entity, idPropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(ConnectionCustom.CONNECTION_STRING, entityList, idPropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connectionString, entityList, idPropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connection, entityList, idPropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(transaction, entityList, idPropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(ConnectionCustom.CONNECTION_STRING, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(string connectionString, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connectionString, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(IConnectionCustom connection, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connection, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public IDbResultCustom insertOrUpdate<T>(ITransactionCustom transaction, T entity, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(transaction, entity, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(ConnectionCustom.CONNECTION_STRING, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(string connectionString, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connectionString, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(IConnectionCustom connection, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(connection, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<IDbResultCustom> insertOrUpdate<T>(ITransactionCustom transaction, List<T> entityList, List<string> idPropertyNameList, List<string> uniquePropertyNameList)
        {
            return entityToCommandCustom.insertOrUpdate(transaction, entityList, idPropertyNameList, uniquePropertyNameList);
        }

        public List<ICommandCustom> delete(string tableName, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.delete(tableName, afterWhereSql, afterWhereSqlParams);
        }

        public List<ICommandCustom> delete<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.delete<T>(afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.query<T>(ConnectionCustom.CONNECTION_STRING, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(string connectionString, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.query<T>(connectionString, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(IConnectionCustom connection, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.query<T>(connection, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(ITransactionCustom transaction, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.query<T>(transaction, afterWhereSql, afterWhereSqlParams);
        }

        public T query<T>(ICommandCustom fwcmd)
        {
            return entityToCommandCustom.query<T>(ConnectionCustom.CONNECTION_STRING, fwcmd);
        }

        public T query<T>(string connectionString, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.query<T>(connectionString, fwcmd);
        }

        public T query<T>(IConnectionCustom connection, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.query<T>(connection, fwcmd);
        }

        public T query<T>(ITransactionCustom transaction, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.query<T>(transaction, fwcmd);
        }

        public List<T> queryList<T>(ICommandCustom fwcmd)
        {
            return entityToCommandCustom.queryList<T>(ConnectionCustom.CONNECTION_STRING, fwcmd);
        }

        public List<T> queryList<T>(string connectionString, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.queryList<T>(connectionString, fwcmd);
        }

        public List<T> queryList<T>(IConnectionCustom connection, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.queryList<T>(connection, fwcmd);
        }

        public List<T> queryList<T>(ITransactionCustom transaction, ICommandCustom fwcmd)
        {
            return entityToCommandCustom.queryList<T>(transaction, fwcmd);
        }

        //public FWPageData<T> queryPage<T>(IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return entityToCommandCustom.queryPage<T>(ConnectionCustom.CONNECTION_STRING, fwPageProcedureParams);
        //}

        //public FWPageData<T> queryPage<T>(string connectionString, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return entityToCommandCustom.queryPage<T>(connectionString, fwPageProcedureParams);
        //}

        //public FWPageData<T> queryPage<T>(IConnectionCustom connection, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return entityToCommandCustom.queryPage<T>(connection, fwPageProcedureParams);
        //}

        //public FWPageData<T> queryPage<T>(ITransactionCustom transaction, IFWPageProcedureParams fwPageProcedureParams)
        //{
        //    return entityToCommandCustom.queryPage<T>(transaction, fwPageProcedureParams);
        //}

        public ICommandCustom updateExcludeNull<T>(T entity, string afterWhereSql, List<IParameterCustom> afterWhereSqlParams)
        {
            return entityToCommandCustom.updateExcludeNull<T>(entity, afterWhereSql, afterWhereSqlParams);
        }
    }
}
