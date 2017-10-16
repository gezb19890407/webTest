using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace System.Data
{
    public interface IDbResultCustom
    {
        EnumDbResultStatus dbResultStatus { get; set; }

        EnumDbAction dbAction { get; set; }
    }
}