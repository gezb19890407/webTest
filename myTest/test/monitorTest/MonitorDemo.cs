using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace myTest.test.monitorTest
{
    public class MonitorDemo
    {
        public static ObjectCache cache = MemoryCache.Default;
        public static string doBusiness(string methodNumber)
        {
            if (cache[methodNumber] == null)
            {
                cache[methodNumber] = "5";
            }
            lock (cache[methodNumber])
            {
                cache[methodNumber] = methodNumber + "1";
            }
            return cache[methodNumber].ToString();
        }
    }
}
