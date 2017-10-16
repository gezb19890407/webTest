using System.Data;
using System;

namespace System.Data
{
    public interface ICommandCustom
    {
        DatabaseTypeCustom DatabaseTypeCustom { get; }

        IDbCommand IDbCommand { get; }

        CommandType CommandType { get; set; }

        string CommandText { get; set; }

        IParameterCollectionCustom Parameters { get; set; }

        Int64 recordCount { get; set; }

        ComparisonTypeCustom ComparisonTypeCustom { get; set; }

        bool comparison(Int64 impactRecordCount);
    }
}