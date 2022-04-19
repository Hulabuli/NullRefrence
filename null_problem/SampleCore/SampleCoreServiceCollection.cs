using Common.Interfaces.StandardUnits;
using Microsoft.Extensions.DependencyInjection;
using SampleCore.Interfaces.Generators;
using SampleCore.Simulator;

namespace SampleCore.Services;

public static class SampleCoreServiceCollection
{
    public static IServiceCollection AddSampleCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<ISampleGenerator>(x => new SampleSimulator(x.GetRequiredService<IStandardUnitTable>()));

        return services;
    }
}