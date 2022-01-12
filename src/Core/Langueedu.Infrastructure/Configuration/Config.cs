using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Langueedu.Infrastructure.Configuration;

public static class Config
{
    public static IEnumerable<IdentityResource> Ids =>
        new IdentityResource[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
        };


    public static IEnumerable<ApiResource> Apis =>
        new ApiResource[]
        {
                // the api requires the role claim
                new ApiResource("langueedu_web", "The Languedu API"),
                new ApiResource("langueedu_api", "The Web API"),
        };

    public static IEnumerable<ApiScope> GetApiScopes => new List<ApiScope>
             {
                 new ApiScope(name: "langueedu_web"),
                 new ApiScope(name: "langueedu_api"),
             };

    public static IEnumerable<Client> Clients(string identityUrl, string identitySecret) =>
        new Client[]
        {
                new Client
                {
                    ClientId = "blazor",
                    ClientName = "Langueedu Client",
                    AccessTokenLifetime = 3600 * 24 * 7,// 7 day
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    Enabled = true,
                    AllowedCorsOrigins = { identityUrl },
                    RedirectUris = { $"{identityUrl}/authentication/login-callback" },
                    PostLogoutRedirectUris = { identityUrl },
                    ClientSecrets =
                    {
                        new Secret(identitySecret.Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "langueedu_web",
                        "langueedu_api",
                    },
                },
        };
}