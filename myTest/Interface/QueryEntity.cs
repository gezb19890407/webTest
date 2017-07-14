using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myTest.Interface
{
    class QueryEntity : IQueryEntity
    {
        public virtual List<string> check()
        {
            List<string> result = new List<string>();
            return result;
        }
    }
}
