using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{
    public class FilterDateTimeDataCustom : FilterObjectDataCustom
    {
        public FilterDateTimeConditionTypeCustom? condition0TypeCode
        {
            get;
            set;
        }

        public DateTime? condition0Value
        {
            get;
            set;
        }

        public FilterConditionRelationTypeCustom? conditionRelationTypeCode
        {
            get;
            set;
        }

        public FilterDateTimeConditionTypeCustom? condition1TypeCode
        {
            get;
            set;
        }

        public DateTime? condition1Value
        {
            get;
            set;
        }
    }
}