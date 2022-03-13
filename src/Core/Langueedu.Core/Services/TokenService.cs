using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Langueedu.Core.Entities.User;
using Langueedu.Core.Interfaces;
using Langueedu.SharedKernel.ViewModels.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
  IConfiguration _configuration;
  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public TokenViewModel GetToken(IUser user)
  {
    var claims = new[]
  {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id),
      new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
      new Claim(JwtRegisteredClaimNames.Email, user.Email)
    };

    DateTime expires = DateTime.UtcNow.AddDays(7);

    var token = new JwtSecurityToken
    (
      issuer: _configuration["Issuer"],
      audience: _configuration["Audience"],
      claims: claims,
      expires: expires,
      notBefore: DateTime.UtcNow,
      signingCredentials: new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
        SecurityAlgorithms.HmacSha256)
    );

    return new TokenViewModel
    {
      AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
      ExpiresIn = expires.Millisecond,
      TokenType = "Bearer",
    };
  }
}