namespace Server.Config;

public interface IConfigurationInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}
