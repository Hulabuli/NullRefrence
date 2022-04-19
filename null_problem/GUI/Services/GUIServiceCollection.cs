using GUI.Interfaces;
using GUI.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GUI.Services;

public static class GUIServiceCollection
{
    public static IServiceCollection AddGUIServices(this IServiceCollection services)
    {
        services.AddPageServices()
                .AddViewModelServices()
                .AddUsercontrolServices()
                .AddModelServices()
                .AddSingleton<ISessionControlNavigationService>(x => new NavigationService(x.GetRequiredService<ISessionControlPageService>()))
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IWindow>(x => new MainNavigationWindow(x.GetRequiredService<INavigationService>(), x.GetRequiredService<ChartModel>()));

        return services;
    }
}