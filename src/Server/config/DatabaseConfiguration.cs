using DataAccess.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Server.config;

public class DatabaseConfiguration : IConfigurationInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccess(configuration.GetConnectionString("DatabaseUrl"));

        services.Configure<DataProtectionTokenProviderOptions>(
            options => { options.TokenLifespan = TimeSpan.FromMinutes(15); }
        );
    }
}
