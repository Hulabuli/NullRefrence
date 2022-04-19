using Common.Interfaces.StandardUnits;
using Common.StandardUnits;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Services;

public static class CommonServiceCollection
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IStandardUnitTable, StandardUnitTable>();

        return services;
    }
}