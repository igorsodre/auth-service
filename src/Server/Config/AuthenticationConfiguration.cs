using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Config.AuthProviders;
using Server.Interfaces;
using Server.Services;

namespace Server.Config;

public class AuthenticationConfiguration : IConfigurationInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        AddJwtTokenConfig(services, configuration);
    }

    private static void AddJwtTokenConfig(IServiceCollection services, IConfiguration configuration)
    {
        var accessTokenSecret = configuration.GetValue<string>("JwtAccessTokenSecret");
        services.Configure<GoogleSettings>(configuration.GetSection("Authentication:Google"));

        services.AddScoped<ITokenService>(
            provider => {
                var cfg = provider.GetRequiredService<IConfiguration>();
                return new TokenService(
                    cfg.GetValue<string>("JwtAccessTokenSecret"),
                    cfg.GetValue<string>("JwtRefreshTokenSecret")
                );
            }
        );

        services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                jwt => {
                    var key = Encoding.ASCII.GetBytes(accessTokenSecret);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 },
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );
    }
}
