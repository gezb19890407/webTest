using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;

namespace System.Data
{
    public class DataBaseCheckParam
    {
        /// <summary>
        /// 过滤不安全的字符串
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string checkParam(string Htmlstring)
        {
            //return Htmlstring;
            if (Htmlstring == null)
            {
                return "";
            }
            else
            {
                //删除脚本
                Htmlstring = Htmlstring.Replace("\r\n", " ");
                Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                //Htmlstring = Htmlstring.Replace("--", "");
                Htmlstring = Htmlstring.Replace(";", "；");
                //删除与数据库相关的词
                Htmlstring = Regex.Replace(Htmlstring, "drop table", "d", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "truncate", "t", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, " mid ", "m", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, " xp_cmdshell ", "x", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, " exec master ", "e", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, " net localgroup administrators ", "n", RegexOptions.IgnoreCase);
                Htmlstring = Htmlstring.Replace("=", " = ");
                Htmlstring = Htmlstring.Replace("\n", " ");
                Htmlstring = Htmlstring.Replace("'", "''");

                //Htmlstring = Htmlstring.Replace(" '", " ¤¤●●");
                //Htmlstring = Htmlstring.Replace("' ", "¤¤●● ");
                //Htmlstring = Htmlstring.Replace("'", "''");
                //Htmlstring = Htmlstring.Replace("¤¤●● ", "' ");
                //Htmlstring = Htmlstring.Replace(" ¤¤●●", " '");
                //Htmlstring = Htmlstring.Replace(" = ", "=");
                //Htmlstring = Htmlstring.Replace("'' ", "' ");
                //Htmlstring = Htmlstring.Replace(" ''", " '");
                //if (Htmlstring.EndsWith("''") && (!Htmlstring.EndsWith("''''")))
                //{
                //    Htmlstring = Htmlstring.Remove(Htmlstring.Length - 2, 2) + "'";
                //}
                return Htmlstring;

            }
        }

        /// <summary>
        /// 过滤对象中不安全字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="Entity">对象</param>
        /// <returns>对象</returns>
        public static T checkParam<T>(T Entity)
        {
            try
            {
                if (Entity != null)
                {
                    return checkParam<T>(Entity, null);
                }
                else { return Entity; }
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(ExceptionHelper.GetErrMsg(ex));
            }
        }

        /// <summary>
        /// 过滤对象中不安全字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="Entity">对象</param>
        /// <returns>对象</returns>
        public static T checkParam<T>(T Entity, bool? IsFormatDateTime)
        {
            try
            {
                Type t = typeof(T);
                if (t.Name == "List`1")//判断是否是List泛型
                {
                    IEnumerable entityList = Entity as IEnumerable;
                    PropertyInfo[] pis = null;
                    List<string> entityList1;
                    foreach (object entity in entityList)
                    {
                        if (entity.GetType() == typeof(String))
                        {
                            entityList1 = new List<string>();
                            entityList1 = entityList.Cast<string>() as List<string>;
                            for (int i = 0; i < entityList1.Count; i++)
                            {
                                entityList1[i] = checkParam(entityList1[i]);
                            }
                            break;
                            //pis[0].SetValue(entity, checkParam(pis[0].GetValue(entity, null).ToString()), null);
                        }
                        else if (entity.GetType().IsGenericType || entity.GetType().IsClass)
                        {
                            pis = null;
                            pis = entity.GetType().GetProperties();
                            checkEntityPropertyInfoSql(pis, entity, IsFormatDateTime);
                        }
                        else
                        {
                            checkParam(entity);
                        }
                    }
                }
                else
                {
                    PropertyInfo[] PropertyInfoS = t.GetProperties();
                    checkEntityPropertyInfoSql(PropertyInfoS, Entity, IsFormatDateTime);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Entity;
        }

        private static void checkEntityPropertyInfoSql(PropertyInfo[] PropertyInfoS, object Entity, bool? IsFormatDateTime)
        {
            foreach (PropertyInfo pi in PropertyInfoS)
            {
                if (pi.PropertyType.IsGenericType || (pi.PropertyType.IsClass && pi.PropertyType != typeof(String)))
                {
                    checkParam(pi.GetValue(Entity, null));
                }
                else if (pi.GetValue(Entity, null) != null)
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        pi.SetValue(Entity, checkParam(pi.GetValue(Entity, null).ToString()), null);
                    }
                    else
                    {
                        if (IsFormatDateTime == true && (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime)))
                        {
                            if (pi.GetValue(Entity, null) != null)
                            {
                                DateTime dtValue = (DateTime)pi.GetValue(Entity, null);
                                if (pi.Name.IndexOf("Start") > 0)
                                {
                                    dtValue = DateTime.Parse(dtValue.ToString("yyyy-MM-dd"));
                                }
                                if (pi.Name.IndexOf("End") > 0)
                                {
                                    dtValue = DateTime.Parse(dtValue.ToString("yyyy-MM-dd 23:59:59"));
                                }
                                pi.SetValue(Entity, dtValue, null);
                            }
                        }
                    }
                }
            }
        }
    }
}
