using System.Text;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebForumAPI.Extensions;

public static class ApiExtensions
{
  public static void AddApiAuthentication(this IServiceCollection serviceCollection,
    JWTOptions jwtOptions)
  {
    serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme, options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
          };

          options.Events = new JwtBearerEvents()
          {
            OnMessageReceived = context =>
            {
              context.Token = context.Request.Cookies["YUMI"];
              return Task.CompletedTask;
            }
          };
        });
    serviceCollection.AddAuthentication();
  }
}