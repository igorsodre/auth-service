using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Server.config;

public class MvcConfigutation : IConfigurationInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddControllers()
            .AddFluentValidation(
                options => {
                    options.DisableDataAnnotationsValidation = true;
                    options.ImplicitlyValidateChildProperties = true;
                    options.RegisterValidatorsFromAssemblyContaining<Program>();
                }
            );
    }
}
