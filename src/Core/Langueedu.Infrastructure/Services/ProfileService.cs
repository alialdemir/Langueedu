using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Langueedu.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Langueedu.Infrastructure.Services;

public class ProfileService : IProfileService
{
    #region Private varibles

    private readonly UserManager<User> _userManager;

    #endregion Private varibles

    #region Constructor

    public ProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    #endregion Constructor

    #region Methods

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

        var user = await _userManager.FindByIdAsync(subjectId);
        if (user == null)
            throw new ArgumentException("Invalid subject identifier");

        IList<string> userRoles = await _userManager.GetRolesAsync(user);

        var claims = GetClaimsFromUser(user, userRoles);
        context.IssuedClaims = claims.ToList();
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

        var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
        var user = await _userManager.FindByIdAsync(subjectId);

        context.IsActive = false;

        if (user != null)
        {
            if (_userManager.SupportsUserSecurityStamp)
            {
                var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                if (security_stamp != null)
                {
                    var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
                    if (db_security_stamp != security_stamp)
                        return;
                }
            }

            context.IsActive =
                !user.LockoutEnabled ||
                !user.LockoutEnd.HasValue ||
                user.LockoutEnd <= DateTime.Now;
        }
    }

    private IEnumerable<Claim> GetClaimsFromUser(User user, IList<string> userRoles)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.Role, string.Join(", ", userRoles)),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

        return claims;
    }

    #endregion Methods
}