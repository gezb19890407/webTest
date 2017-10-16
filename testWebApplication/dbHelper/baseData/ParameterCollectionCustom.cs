using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{
    public class ParameterCollectionCustom : List<IParameterCustom>, IParameterCollectionCustom
    {
        public new IParameterCustom Add(IParameterCustom value)
        {
            if (value != null)
            {
                IParameterCustom iParameterCustom = GetParameter(value.ParameterName);
                if (iParameterCustom != null)
                {
                    iParameterCustom.Value = value.Value;
                }
                else
                {
                    base.Add(value);
                }
            }
            return value;
        }

        public IParameterCustom AddWithValue(string parameterName, object value)
        {
            return this.Add(new ParameterCustom(parameterName, value));
        }

        public IParameterCustom AddWithValue(string parameterName, object value, Type valueType)
        {
            return this.Add(new ParameterCustom(parameterName, value, valueType));
        }

        public IParameterCustom this[string parameterName]
        {
            get
            {
                return GetParameter(parameterName);
            }
            set
            {
                SetParameter(parameterName, value);
            }
        }

        protected IParameterCustom GetParameter(string parameterName)
        {
            return this.FirstOrDefault(e => e.ParameterName.Equals(parameterName));
        }

        protected void SetParameter(string parameterName, object value)
        {
            this.Add(new ParameterCustom(parameterName, value));
        }
    }
}