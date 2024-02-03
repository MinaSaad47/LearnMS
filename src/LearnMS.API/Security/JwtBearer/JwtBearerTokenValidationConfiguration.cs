
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearnMS.API.Security.JwtBearer;


public sealed class JwtBearerTokenValidationConfiguration : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtBearerConfig _config;

    public JwtBearerTokenValidationConfiguration(IOptions<JwtBearerConfig> options)
    {
        _config = options.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config.Issuer,
            ValidAudience = _config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_config.Secret)),
        };
    }
}