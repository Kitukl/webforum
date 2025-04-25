using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application;
using Microsoft.Extensions.Options;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public class JWTProvider : IJWTProvider
{
  private readonly JWTOptions _options;

  public JWTProvider(IOptions<JWTOptions> options)
  {
    _options = options.Value;
  }

  public string GenerateToken(User user)
  {
    Claim[] claims = [new("userId", user.Id.ToString()), new(ClaimTypes.Role, user.Role)];
    
    var signingCreedentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(
      claims: claims,
      signingCredentials: signingCreedentials,
      expires: DateTime.UtcNow.AddHours(_options.Expires)
      );
    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
    return tokenValue;
  }
}
