using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerAspNetCore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using IdentityModel.Client;

namespace IdentityServerAspNetCore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Values()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var valuesResponse = await (await client.GetAsync($"http://localhost:54743/api/values")).Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<string[]>(valuesResponse);

                return View(values);
            }
        }

        private async Task RefreshToken()
        {
            // Verificar este exemplo: https://github.com/mderriey/aspnet-core-token-renewal/blob/master/src/MvcClient/Startup.cs

            //var http = new HttpClient();
            //var disco = await http.GetDiscoveryDocumentAsync("http://localhost:52047");
            //var refreshToken = disco.TokenEndpoint.ref;
            //var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
            //{
            //    Address = "https://localhost:44347/connect/token",
            //    ClientId = "mvc",
            //    ClientSecret = "mvc",
            //    RefreshToken = refreshToken
            //});

            //if (!response.IsError)
            //{
            //    // everything went right, remove old tokens and add new ones
            //    identity.RemoveClaim(accessTokenClaim);
            //    identity.RemoveClaim(refreshTokenClaim);

            //    identity.AddClaims(new[]
            //    {
            //                            new Claim("access_token", response.AccessToken),
            //                            new Claim("refresh_token", response.RefreshToken)
            //                        });

            //    // indicate to the cookie middleware to renew the session cookie
            //    // the new lifetime will be the same as the old one, so the alignment
            //    // between cookie and access token is preserved
            //    x.ShouldRenew = true;
            //}

            ////DiscoveryClient
            ////DiscoveryClient.GetAsync

            //var http = new HttpClient();
            //// discover endpoints from metadata
            //var disco = await http.GetDiscoveryDocumentAsync("http://localhost:52047");


            //var client = new TokenClient(null, disco.TokenEndpoint);
            //var refresh_token = await HttpContext.GetTokenAsync("refresh_token");
            //var tokenResponse = await client.RequestRefreshTokenAsync(refresh_token);
            //var identityToken = await HttpContext.GetTokenAsync("id_token");

            //var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);

            //var tokens = new[]
            //{
            //    new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.IdToken,
            //        Value = identityToken
            //    },
            //    new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.AccessToken,
            //        Value = tokenResponse.AccessToken
            //    },
            //    new AuthenticationToken
            //    {
            //        Name = OpenIdConnectParameterNames.RefreshToken,
            //        Value = tokenResponse.RefreshToken
            //    },
            //    new AuthenticationToken
            //    {
            //        Name = "expires_at",
            //        Value = expiresAt.ToString("o", CultureInfo.InvariantCulture)
            //    }
            //};

            //var authenticationInformation = HttpContext.AuthenticateAsync("Cookies");

            //authenticationInformation.Result.Properties.StoreTokens(tokens);

            //await HttpContext.SignInAsync("Cookies",
            //    authenticationInformation.Result.Principal,
            //    authenticationInformation.Result.Properties);

        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
