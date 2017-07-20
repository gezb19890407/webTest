using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace utilityClass.generic
{
    public class GenericData
    {
        public static string BaseDirectory
        {
            get
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}