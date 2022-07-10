using Server.Config;

namespace Server.Extensions.Configuration;

public static class InstallerExtensions
{
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationInstallers = typeof(Program).Assembly.ExportedTypes
            .Where((x) => typeof(IConfigurationInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IConfigurationInstaller>()
            .ToList();

        foreach (var installer in configurationInstallers)
        {
            installer.InstallServices(services, configuration);
        }
    }
}
