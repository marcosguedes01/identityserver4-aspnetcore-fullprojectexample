using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

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

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "identityserverfullexample",
                    ClientName = "Identity Server Example",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "identityserverfullexample" }
                },
                new Client
                {
                    ClientId = "identityserverfullexample_implicit",
                    ClientName = "Identity Server Example Implicit",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "identityserverfullexample"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new [] { "http://localhost:62990/signin-oidc" },
                    PostLogoutRedirectUris = new [] { "http://localhost:62990/signout-callback-oidc" }
                },
                new Client
                {
                    ClientId = "identityserverfullexample_code",
                    ClientName = "Identity Server Example Code",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    //LogoUri = "http://github.githubassets.com/images/modules/logos_page/Octocat.png",
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "identityserverfullexample",
                        "offline_access"
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new [] { "http://localhost:62990/signin-oidc" },
                    PostLogoutRedirectUris = new [] { "http://localhost:62990/signout-callback-oidc" }
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
                    Password = "password",
                    Claims = new [] { new Claim("email", "usertest@domain.com") }
                }
            };
        }
    }
}
