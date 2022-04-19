using GUI.Interfaces;
using GUI.Pages;
using GUI.Pages.SessionControl;
using GUI.ViewModels;
using GUI.ViewModels.SessionControl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Services;

public static class PageServiceCollection
{
    public static IServiceCollection AddPageServices(this IServiceCollection services)
    {
        //Main pages
        services.AddSingleton<ControlCenterPage>();
        services.AddSingleton<HomePage>();
        services.AddSingleton<SessionControlPage>();
        services.AddSingleton<ChartPage>();
        services.AddSingleton<GroupStatisticsPage>();
        services.AddSingleton<SensorValuePage>();

        //SessionControl
        services.AddSingleton<AdvancedSessionPage>();

        //Dictionaries
        services.AddSingleton<IPageService>(x => new PageService(BuildPageDictionary(x)));
        services.AddSingleton<ISessionControlPageService>(x => new PageService(BuildSessionControlPageDictionary(x)));

        return services;
    }

    private static Dictionary<Type, Func<Page>> BuildPageDictionary(IServiceProvider serviceProvider)
    {
        return new()
        {
            // Main navigation pages
            { typeof(HomePageViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<HomePage>()) },
            { typeof(SessionControlViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<SessionControlPage>()) },
            { typeof(ControlCenterViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<ControlCenterPage>()) },
            { typeof(SensorValueViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<SensorValuePage>()) },
            { typeof(ChartViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<ChartPage>()) },
            { typeof(GroupStatisticsViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<GroupStatisticsPage>()) },

            // SessionControl navigation pages
            { typeof(AdvancedSessionViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<AdvancedSessionPage>()) },
        };
    }


    private static Dictionary<Type, Func<Page>> BuildSessionControlPageDictionary(IServiceProvider serviceProvider)
    {
        return new()
        {
            { typeof(SessionControlViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<SessionControlPage>()) },
            { typeof(AdvancedSessionViewModel), new Func<Page>(() => serviceProvider.GetRequiredService<AdvancedSessionPage>()) }
        };
    }
}