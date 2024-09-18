using GameShop.Infrastructure.Read.Data;
using GameShop.Read.IntegrationTests.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Read.IntegrationTests;
public abstract class ApiBaseTests
{
    protected ApiBaseTests()
    {
        var app = new CustomWebApplicationFactory(services =>
        {
        });
        var scopeProvider = app.Services.GetService<IServiceScopeFactory>()!.CreateScope().ServiceProvider!;
        ApiClient = app.CreateClient();
        ReadOnlyDbContext = scopeProvider.GetService<ReadOnlyDatabaseContext>()!;
    }

    public HttpClient ApiClient { get; init; }

    public ReadOnlyDatabaseContext ReadOnlyDbContext { get; init; }
}
