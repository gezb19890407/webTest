using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{
    public static class ConfigHelper
    {
        public static string getValue(string key)
        {
            object value = System.Configuration.ConfigurationManager.ConnectionStrings[key];
            if (value == null)
            {
                return "";
            }
            return value.ToString().Trim();
        }
    }
}