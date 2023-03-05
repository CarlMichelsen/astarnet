using BusinessLogic.Calculators;
using BusinessLogic.Calculators.Interface;
using Microsoft.Extensions.DependencyInjection;
using Test.BusinessLogicTest.Testdata;

namespace Test.BusinessLogicTest;

public static class NodesetTestDependencies
{
    public static IServiceCollection RegisterNodesetTestDependencies(this IServiceCollection services)
    {
        // Add your dependencies to the container
        services.AddScoped<StraightTestNodeset>();
        services.AddScoped<IPathCalculator, PathCalculator>();

        return services;
    }
}