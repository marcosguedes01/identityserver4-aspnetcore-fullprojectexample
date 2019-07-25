using Dapper;
using IdentityServerAspNetCore.Data.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetCore.Data.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(Func<IDbConnection> openConnection) : base(openConnection)
        {
        }

        public async Task<User> GetAsync(string username, string password)
        {
            using (var connection = OpenConnection())
            {
                var queryResult = await connection.QueryAsync<User>("SELECT * FROM [Users] WHERE Username=@username AND Password=@password", new {  username, password });

                return queryResult.SingleOrDefault();
            }
        }
    }
}
