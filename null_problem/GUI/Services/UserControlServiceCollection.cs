using GUI.Interfaces;
using GUI.UserControls;
using GUI.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Services;

public static class UserControlServiceCollection
{
    public static IServiceCollection AddUsercontrolServices(this IServiceCollection services)
    {
        services.AddTransient<ChannelBlockUC>();
        services.AddSingleton<ChartUC>();
        services.AddSingleton<IUserControlFactory>(x => new UserControlFactory(BuildUserControlDictionary(x)));

        return services;
    }

    private static Dictionary<Type, Func<UserControl>> BuildUserControlDictionary(IServiceProvider serviceProvider)
    {
        return new()
        {
            { typeof(ChannelBlockViewModel), new Func<UserControl>(() => serviceProvider.GetRequiredService<ChannelBlockUC>()) },
        };
    }
}