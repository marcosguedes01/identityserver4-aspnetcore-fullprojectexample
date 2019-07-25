using IdentityServerApnetCore.OAuth.Helpers;
using IdentityServerAspNetCore.Data.Models;
using IdentityServerAspNetCore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerApnetCore.OAuth.Configuration
{
    public class UserValidator : IUserValidator
    {
        private IUserRepository repository;

        public UserValidator(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByUsernameAsync(string username)
        {
            return repository.GetAsync(username);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            return (await repository.GetAsync(username, HashHelper.Sha512(password + username))) != null;
        }
    }

    public interface IUserValidator
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByExternalProviderAsync(string provider, string userId);
        Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims);
    }
}
