using System.Data;

namespace System.Data
{
    public interface ITransactionCustom
    {
        DatabaseTypeCustom DatabaseTypeCustom { get; }

        IDbTransaction IDbTransaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();

        void Open();

        void Close();
    }
}