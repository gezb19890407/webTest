using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System;
using System.Text;

namespace System.Data
{
    public class CommandStaticHelper
    {
        public static DatabaseTypeCustom getDatabaseType()
        {
            return getDatabaseType(null);
        }

        public static DatabaseTypeCustom getDatabaseType(string connectionString)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(connectionString);
            return fwCommandHelper.DatabaseTypeCustom;
        }

        public static bool isCanConnection(string connectionString)
        {
            CommandCustomHelper _FWCommandHelper = new CommandCustomHelper(connectionString);
            return _FWCommandHelper.isCanConnection(connectionString);
        }

        public static bool isCanConnection()
        {
            return isCanConnection(null);
        }

        public static string checkParam(DatabaseTypeCustom DatabaseTypeCustom, string param)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(DatabaseTypeCustom);
            return fwCommandHelper.checkParam(param);
        }

        public static string checkParam(string param)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.checkParam(param);
        }

        public static string joinToSqlString<T>(List<T> tList)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.joinToSqlString(tList);
        }

        public static string convertToSqlString<T>(string conditionColumnName, List<T> tList)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(conditionColumnName))
            {
                string sqlString = joinToSqlString(tList);
                if (!string.IsNullOrEmpty(sqlString))
                {
                    sb.AppendFormat("{0} in ({1})", conditionColumnName, sqlString);
                }
            }
            return sb.ToString();
        }

        public static string convertToSqlString(string conditionColumnName, FilterObjectDataCustom fwFilterObjectData)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(conditionColumnName) && fwFilterObjectData != null)
            {
                StringBuilder sb0 = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                FilterConditionRelationTypeCustom? conditionRelationTypeCode = null;
                if (fwFilterObjectData.GetType() == typeof(FilterDateTimeDataCustom))
                {
                    FilterDateTimeDataCustom fwFilterDateTimeData = (FilterDateTimeDataCustom)fwFilterObjectData;

                    if (fwFilterDateTimeData.condition0TypeCode.HasValue && fwFilterDateTimeData.condition0Value.HasValue)
                    {
                        switch (fwFilterDateTimeData.condition0TypeCode.Value)
                        {
                            case FilterDateTimeConditionTypeCustom.Equal:
                                sb0.AppendFormat("{0}='{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.NotEqual:
                                sb0.AppendFormat("{0}<>'{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.Than:
                                sb0.AppendFormat("{0}>'{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.ThanEqual:
                                sb0.AppendFormat("{0}>='{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.Less:
                                sb0.AppendFormat("{0}<'{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.LessEqual:
                                sb0.AppendFormat("{0}<='{1}'", conditionColumnName, fwFilterDateTimeData.condition0Value.Value);
                                break;
                        }
                    }
                    conditionRelationTypeCode = fwFilterDateTimeData.conditionRelationTypeCode;
                    if (fwFilterDateTimeData.condition1TypeCode.HasValue && fwFilterDateTimeData.condition1Value.HasValue)
                    {
                        switch (fwFilterDateTimeData.condition1TypeCode.Value)
                        {
                            case FilterDateTimeConditionTypeCustom.Equal:
                                sb1.AppendFormat("{0}='{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.NotEqual:
                                sb1.AppendFormat("{0}<>'{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.Than:
                                sb1.AppendFormat("{0}>'{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.ThanEqual:
                                sb1.AppendFormat("{0}>='{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.Less:
                                sb1.AppendFormat("{0}<'{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                            case FilterDateTimeConditionTypeCustom.LessEqual:
                                sb1.AppendFormat("{0}<='{1}'", conditionColumnName, fwFilterDateTimeData.condition1Value.Value);
                                break;
                        }
                    }

                }
                //else if (fwFilterObjectData.GetType() == typeof(FWFilterNumberData))
                //{
                //    FWFilterNumberData fwFilterNumberData = (FWFilterNumberData)fwFilterObjectData;

                //    if (fwFilterNumberData.condition0TypeCode.HasValue && fwFilterNumberData.condition0Value.HasValue)
                //    {
                //        switch (fwFilterNumberData.condition0TypeCode.Value)
                //        {
                //            case FWFilterNumberConditionType.Equal:
                //                sb0.AppendFormat("{0}='{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.NotEqual:
                //                sb0.AppendFormat("{0}<>'{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.Than:
                //                sb0.AppendFormat("{0}>'{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.ThanEqual:
                //                sb0.AppendFormat("{0}>='{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.Less:
                //                sb0.AppendFormat("{0}<'{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.LessEqual:
                //                sb0.AppendFormat("{0}<='{1}'", conditionColumnName, fwFilterNumberData.condition0Value.Value);
                //                break;
                //        }
                //    }
                //    conditionRelationTypeCode = fwFilterNumberData.conditionRelationTypeCode;
                //    if (fwFilterNumberData.condition1TypeCode.HasValue && fwFilterNumberData.condition1Value.HasValue)
                //    {
                //        switch (fwFilterNumberData.condition1TypeCode.Value)
                //        {
                //            case FWFilterNumberConditionType.Equal:
                //                sb1.AppendFormat("{0}='{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.NotEqual:
                //                sb1.AppendFormat("{0}<>'{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.Than:
                //                sb1.AppendFormat("{0}>'{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.ThanEqual:
                //                sb1.AppendFormat("{0}>='{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.Less:
                //                sb1.AppendFormat("{0}<'{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //            case FWFilterNumberConditionType.LessEqual:
                //                sb1.AppendFormat("{0}<='{1}'", conditionColumnName, fwFilterNumberData.condition1Value.Value);
                //                break;
                //        }
                //    }
                //}
                //else if (fwFilterObjectData.GetType() == typeof(FWFilterStringData))
                //{
                //    FWFilterStringData fwFilterStringData = (FWFilterStringData)fwFilterObjectData;

                //    if (fwFilterStringData.condition0TypeCode.HasValue && !string.IsNullOrEmpty(fwFilterStringData.condition0Value))
                //    {
                //        switch (fwFilterStringData.condition0TypeCode.Value)
                //        {
                //            case FWFilterStringConditionType.Equal:
                //                sb0.AppendFormat("{0}='{1}'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //            case FWFilterStringConditionType.NotEqual:
                //                sb0.AppendFormat("{0}<>'{1}'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //            case FWFilterStringConditionType.StartWith:
                //                sb0.AppendFormat("{0} like '{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //            case FWFilterStringConditionType.EndWith:
                //                sb0.AppendFormat("{0} like '%{1}'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //            case FWFilterStringConditionType.Contain:
                //                sb0.AppendFormat("{0} like '%{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //            case FWFilterStringConditionType.NotContain:
                //                sb0.AppendFormat("{0} not like '%{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition0Value));
                //                break;
                //        }
                //    }
                //    conditionRelationTypeCode = fwFilterStringData.conditionRelationTypeCode;
                //    if (fwFilterStringData.condition1TypeCode.HasValue && !string.IsNullOrEmpty(fwFilterStringData.condition1Value))
                //    {
                //        switch (fwFilterStringData.condition1TypeCode.Value)
                //        {
                //            case FWFilterStringConditionType.Equal:
                //                sb1.AppendFormat("{0}='{1}'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //            case FWFilterStringConditionType.NotEqual:
                //                sb1.AppendFormat("{0}<>'{1}'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //            case FWFilterStringConditionType.StartWith:
                //                sb1.AppendFormat("{0} like '{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //            case FWFilterStringConditionType.EndWith:
                //                sb1.AppendFormat("{0} like '%{1}'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //            case FWFilterStringConditionType.Contain:
                //                sb1.AppendFormat("{0} like '%{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //            case FWFilterStringConditionType.NotContain:
                //                sb1.AppendFormat("{0} not like '%{1}%'", conditionColumnName, checkParam(fwFilterStringData.condition1Value));
                //                break;
                //        }
                //    }
                //}

                if (sb0.Length > 0 && sb1.Length > 0)
                {
                    if (!conditionRelationTypeCode.HasValue)
                    {
                        conditionRelationTypeCode = FilterConditionRelationTypeCustom.And;
                    }
                    switch (conditionRelationTypeCode)
                    {
                        case FilterConditionRelationTypeCustom.And:
                            sb.AppendFormat("({0} and {1})", sb0.ToString(), sb1.ToString());
                            break;
                        case FilterConditionRelationTypeCustom.Or:
                            sb.AppendFormat("({0} or {1})", sb0.ToString(), sb1.ToString());
                            break;
                    }
                }
                else if (sb0.Length > 0)
                {
                    sb.AppendFormat("({0})", sb0.ToString());
                }
                else if (sb1.Length > 0)
                {
                    sb.AppendFormat("({0})", sb1.ToString());
                }
            }
            return sb.ToString();
        }

        public static string getDbTypeString(DatabaseTypeCustom DatabaseTypeCustom, IDbTypeCustom dbType)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(DatabaseTypeCustom);
            return fwCommandHelper.getDbTypeString(dbType);
        }

        public static Type getValueType(DatabaseTypeCustom DatabaseTypeCustom, IDbTypeCustom dbType)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(DatabaseTypeCustom);
            return fwCommandHelper.getValueType(dbType);
        }

        public static int ExecuteScalar(ITransactionCustom Transaction, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Transaction);
            return fwCommandHelper.ExecuteScalar(Transaction, Command);
        }

        public static int ExecuteScalar(IConnectionCustom Connection, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Connection);
            return fwCommandHelper.ExecuteScalar(Connection, Command);
        }

        public static int ExecuteScalar(string connectionString, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(connectionString);
            return fwCommandHelper.ExecuteScalar(connectionString, Command);
        }

        public static int ExecuteScalar(ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteScalar(Command);
        }

        public static int ExecuteNonQuery(ITransactionCustom Transaction, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Transaction);
            return fwCommandHelper.ExecuteNonQuery(Transaction, Command);
        }

        public static int ExecuteNonQuery(IConnectionCustom Connection, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Connection);
            return fwCommandHelper.ExecuteNonQuery(Connection, Command);
        }

        public static int ExecuteNonQuery(string connectionString, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(connectionString);
            return fwCommandHelper.ExecuteNonQuery(connectionString, Command);
        }

        public static int ExecuteNonQuery(ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteNonQuery(Command);
        }

        public static DataSet ExecuteDataSet(ITransactionCustom Transaction, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Transaction);
            return fwCommandHelper.ExecuteDataSet(Transaction, Command);
        }

        public static DataSet ExecuteDataSet(IConnectionCustom Connection, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Connection);
            return fwCommandHelper.ExecuteDataSet(Connection, Command);
        }

        public static DataSet ExecuteDataSet(string connectionString, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(connectionString);
            return fwCommandHelper.ExecuteDataSet(connectionString, Command);
        }

        public static DataSet ExecuteDataSet(ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteDataSet(Command);
        }

        public static DataTable ExecuteDataTable(ITransactionCustom Transaction, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Transaction);
            return fwCommandHelper.ExecuteDataTable(Transaction, Command);
        }

        public static DataTable ExecuteDataTable(IConnectionCustom Connection, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(Connection);
            return fwCommandHelper.ExecuteDataTable(Connection, Command);
        }

        public static DataTable ExecuteDataTable(string connectionString, ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper(connectionString);
            return fwCommandHelper.ExecuteDataTable(connectionString, Command);
        }

        public static DataTable ExecuteDataTable(ICommandCustom Command)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteDataTable(Command);
        }

        public static bool ExecuteNonQuery(ITransactionCustom Transaction, List<ICommandCustom> CommandList)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteNonQuery(Transaction, CommandList);
        }

        public static bool ExecuteNonQuery(IConnectionCustom Connection, List<ICommandCustom> CommandList)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteNonQuery(Connection, CommandList);
        }

        public static bool ExecuteNonQuery(string connectionString, List<ICommandCustom> CommandList)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteNonQuery(connectionString, CommandList);
        }

        public static bool ExecuteNonQuery(List<ICommandCustom> CommandList)
        {
            CommandCustomHelper fwCommandHelper = new CommandCustomHelper();
            return fwCommandHelper.ExecuteNonQuery(CommandList);
        }

        public static bool getTableWhetherExistsResult<T>(string afterWhereSql)
        {
            return SqlCommandStaticHelper.ExecuteScalar(getTableWhetherExistsFWCommand<T>(afterWhereSql)) > 0;
        }

        public static ICommandCustom getTableWhetherExistsFWCommand<T>(string afterWhereSql)
        {
            ICommandCustom cmd = new CommandCustom();
            StringBuilder sbSql = new StringBuilder();
            Type t = typeof(T);
            sbSql.AppendFormat("select count(*) from {0} where {1}", t.Name, afterWhereSql);
            cmd.CommandText = sbSql.ToString();
            return cmd;
        }

        public static string getTableFirstColumnValue(ICommandCustom command)
        {
            DataTable dataTable = ExecuteDataTable(command);
            if (dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
            {
                return dataTable.Rows[0][0].ToString();
            }
            return "";
        }
    }
}