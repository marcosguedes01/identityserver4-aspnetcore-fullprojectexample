﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServerApnetCore.OAuth.Helpers;
using IdentityServerAspNetCore.Data.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerApnetCore.OAuth.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserRepository repository;

        public ResourceOwnerPasswordValidator(IUserRepository repository) {
            this.repository = repository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await repository.GetAsync(context.UserName, HashHelper.Sha512(context.Password + context.UserName));

            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), authenticationMethod: "custom", claims: user.Claims);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials");
            }
        }
    }
}
