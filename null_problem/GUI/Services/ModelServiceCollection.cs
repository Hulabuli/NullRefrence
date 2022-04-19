using GUI.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GUI.Services;

public static class ModelServiceCollection
{
    public static IServiceCollection AddModelServices(this IServiceCollection services)
    {
        services.AddSingleton<ChartModel>();
        services.AddSingleton<CurrentSession>();

        return services;
    }
}