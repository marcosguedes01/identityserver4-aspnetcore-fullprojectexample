using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IdentityServerAspNetCore.Data.Repositories
{
    public abstract class Repository
    {
        private Func<IDbConnection> openConnection;

        public Repository(Func<IDbConnection> openConnection)
        {
            this.openConnection = openConnection;
        }

        protected IDbConnection OpenConnection()
        {
            return openConnection();
        }
    }
}
