using IdentityServerAspNetCore.Data.Models;
using System.Threading.Tasks;

namespace IdentityServerAspNetCore.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string username, string password);
    }
}
