using GUI.Interfaces;
using GUI.ViewModels;
using GUI.ViewModels.SessionControl;
using GUI.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;

namespace GUI.Services;

public static class ViewModelServiceCollection
{
    public static IServiceCollection AddViewModelServices(this IServiceCollection services)
    {
        //Pages ViewModels
        services.AddSingleton<ControlCenterViewModel>();
        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<SessionControlViewModel>();
        services.AddSingleton<ChartViewModel>();
        services.AddSingleton<GroupStatisticsViewModel>();
        services.AddSingleton<SensorValueViewModel>();

        //SessionControl ViewModels
        services.AddSingleton<AdvancedSessionViewModel>();

        //Usercontrol ViewModels
        services.AddTransient<ChannelBlockViewModel>();
        services.AddSingleton<ChartUCViewModel>();

        return services;
    }
}