using Microsoft.Extensions.DependencyInjection;

namespace Test.BusinessLogicTest;

public static class TestDependencies
{
    public static IServiceCollection RegisterTestDependencies(this IServiceCollection services)
    {
        // Add your dependencies to the container
        //services.AddScoped<IDbContext, MyDbContext>();

        return services;
    }
}