using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{

    public interface IParameterCollectionCustom : IList<IParameterCustom>
    {
        new IParameterCustom Add(IParameterCustom value);

        IParameterCustom AddWithValue(string parameterName, object value);

        IParameterCustom AddWithValue(string parameterName, object value, Type valueType);

        IParameterCustom this[string parameterName]
        {
            get;
            set;
        }
    }
}