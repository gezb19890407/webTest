using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace testWebApplication.work.amazonSync.helper
{
    public class DbBase : IDbBase
    {
        DbCommand IDbBase.CreateCommand()
        {
            return new SqlCommand();
        }
        DbConnection IDbBase.CreateConnection()
        {
            return new SqlConnection();
        }
        DbDataAdapter IDbBase.CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }
        DbParameter IDbBase.CreateParameter()
        {
            return new SqlParameter();
        }
        protected string Pre;
        public DbBase()
        {
            Pre = "";
            DbHelper.Provider = this;
        }
    }
}
