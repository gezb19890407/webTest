using System.Data;

namespace System.Data
{
    public interface IConnectionCustom
    {
        DatabaseTypeCustom DatabaseTypeCustom { get; }

        string connectionString { get; }

        IDbConnection iDbConnection { get; }

    }
}