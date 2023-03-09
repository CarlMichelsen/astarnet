using Microsoft.Extensions.DependencyInjection;

namespace Astar.BusinessLogicTest;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Configure your services here
        services.RegisterNodesetTestDependencies();
    }
}