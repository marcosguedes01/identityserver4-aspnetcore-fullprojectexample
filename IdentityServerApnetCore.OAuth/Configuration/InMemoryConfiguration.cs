using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServerApnetCore.OAuth.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("identityserverfullexample", "Identity Server 4 Full Example")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "identityserverfullexample",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "identityserverfullexample" }
                }
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId= "1",
                    Username = "usertest@domain.com",
                    Password = "password"
                }
            };
        }
    }
}
