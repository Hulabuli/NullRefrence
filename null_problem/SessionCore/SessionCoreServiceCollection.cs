using Microsoft.Extensions.DependencyInjection;
using SessionCore.Handlers;
using SessionCore.Interfaces;
using SessionCore.Modes;

namespace SessionCore.Services;

public static class SampleCoreServiceCollection
{
    public static IServiceCollection AddSessionCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<ISetupModeFactory, SetupModeFactory>();
        services.AddSingleton<ISessionFactory, SessionFactory>();
        services.AddSingleton<ISessionManager, SessionManager>();

        return services;
    }
}