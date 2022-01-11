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
                new ApiResource("api", "The Web API"),
                new ApiResource("langueedu", "The Languedu API"/*, new[] { JwtClaimTypes.Role }*/)
        };

    public static IEnumerable<ApiScope> GetApiScopes => new List<ApiScope>
             {
                 new ApiScope(name: "api")
             };

    public static IEnumerable<Client> Clients(string identityUrl) =>
        new Client[]
        {
                new Client
                {
                      ClientId = "blazor",
                            ClientName = "Langueedu Client",
                            AccessTokenLifetime = 3600 * 24 * 90,// 3 month
                            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                            RequireClientSecret = false,
                            AllowOfflineAccess = true,
                            AllowedScopes = new List<string>
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                IdentityServerConstants.StandardScopes.OfflineAccess,
                                "api",
                                "langueedu",
                            },
                },
        };
}

public class ProfileWithRoleIdentityResource : IdentityResources.Profile
{
    public ProfileWithRoleIdentityResource()
    {
        this.UserClaims.Add(JwtClaimTypes.Role);
    }
}