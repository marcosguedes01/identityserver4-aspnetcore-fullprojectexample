﻿using IdentityServerApnetCore.OAuth.Helpers;
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

        public async Task<User> AutoProvisionUserAsync(string provider, string userId, IEnumerable<Claim> claims)
        {
            var user = new User
            {
                Id = Convert.ToInt32(DateTime.Now.ToString("yyyyMMddHmmss")),
                Username = userId,
                Password = HashHelper.Sha512(Guid.NewGuid().ToString() + userId)
            };

            await repository.AddAsync(user);

            return user;
        }

        public Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            return FindByUsernameAsync(userId);
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
