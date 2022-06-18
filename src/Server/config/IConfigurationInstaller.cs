namespace Server.config;

public interface IConfigurationInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}
