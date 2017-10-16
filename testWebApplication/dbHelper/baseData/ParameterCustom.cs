using System;
using System.Data;

namespace System.Data
{
    public class ParameterCustom : IParameterCustom
    {
        public ParameterCustom(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public ParameterCustom(string parameterName, object value, Type parameterType)
        {
            ParameterName = parameterName;
            Value = value;
            ParameterType = parameterType;
        }

        public string ParameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }
        private string _parameterName;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private object _value;

        public Type ParameterType
        {
            get { return _ParameterType; }
            set { _ParameterType = value; }
        }
        private Type _ParameterType;


    }
}