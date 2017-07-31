using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.iterator
{
    public interface Iterator
    {
        bool hasNext();

        object next();
    }
}