using GameShop.Commands.IntegrationTests.Configuration;
using GameShop.Infrastructure.Data;
using GameShop.Infrastructure.EmailSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace GameShop.Commands.IntegrationTests;

public abstract class ApiBaseTests
{
    protected ApiBaseTests()
    {
        var fixture = new Fixture();
        MockEmailLogger = fixture.Freeze<Mock<ILogger<FakeEmailSender>>>();
        var app = new CustomWebApplicationFactory(services =>
        {
            services
                    .Replace(ServiceDescriptor.Transient(_ => MockEmailLogger.Object));
        });
        var scopeProvider = app.Services.GetService<IServiceScopeFactory>()!.CreateScope().ServiceProvider!;
        ApiClient = app.CreateClient();
        ReadOnlyDbContext = scopeProvider.GetService<ReadOnlyDatabaseContext>()!;
        WriteReadDbContext = scopeProvider.GetService<WriteReadDatabaseContext>()!;
        //DatabaseSeed.SeedData(DbContext);
    }

    public HttpClient ApiClient { get; init; }

    public ReadOnlyDatabaseContext ReadOnlyDbContext { get; init; }

    public WriteReadDatabaseContext WriteReadDbContext { get; init; }

    public Mock<ILogger<FakeEmailSender>> MockEmailLogger { get; init; }
}
