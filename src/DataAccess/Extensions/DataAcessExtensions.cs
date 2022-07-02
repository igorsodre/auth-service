using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class DataAcessExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(
            options => {
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
                );
            }
        );

        services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();
        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());
        services.AddScoped<IAccountManager, AccountManager>();
        return services;
    }
}
