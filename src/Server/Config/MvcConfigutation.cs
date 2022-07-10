using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Middlewares;
using Server.Services;

namespace Server.Config;

public class MvcConfigutation : IConfigurationInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        services.AddControllers(options => { options.Filters.Add<IncomingRequestDataValidationMiddleware>(); })
            .AddFluentValidation(
                options => {
                    options.DisableDataAnnotationsValidation = true;
                    options.ImplicitlyValidateChildProperties = true;
                    options.RegisterValidatorsFromAssemblyContaining<Program>();
                }
            );

        services.AddCors(
            options => {
                options.AddDefaultPolicy(
                    policyBuilder => {
                        policyBuilder.WithOrigins(configuration["Cors:AllowedOrigins"])
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    }
                );
            }
        );

        AddDependencyInjection(services);
    }

    private static void AddDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<IGoogleAccountService, GoogleAccountService>();
        services.AddScoped<IExternalAuthService, ExternalAuthService>();
    }
}
