using Microsoft.Extensions.DependencyInjection;

namespace Test.BusinessLogicTest;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Configure your services here
        services.RegisterNodesetTestDependencies();
    }
}