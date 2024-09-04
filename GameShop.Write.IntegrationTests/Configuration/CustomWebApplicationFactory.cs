using GameShop.API.Write;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Write.IntegrationTests.Configuration;

public class CustomWebApplicationFactory(
    Action<IServiceCollection>? overrideDependencies = null)
    : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _overrideDependencies = overrideDependencies;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        builder.ConfigureTestServices(services =>
        {
            _overrideDependencies?.Invoke(services);
            MockDatabaseContext.ReplaceDatabaseContext(services);
        });
    }
}
