using Astar.BusinessLogic.Calculators;
using Astar.BusinessLogic.Calculators.Interface;
using Microsoft.Extensions.DependencyInjection;
using Astar.BusinessLogicTest.Testdata;

namespace Astar.BusinessLogicTest;

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