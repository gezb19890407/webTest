using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{

    public interface IParameterCustom
    {

        string ParameterName { get; set; }

        object Value { get; set; }

        Type ParameterType { get; set; }
    }
}