using GameShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameShop.Read.IntegrationTests.Configuration;

public static class MockDatabaseContext
{
    public static void ReplaceDatabaseContext(IServiceCollection services)
    {
        var readOnlyDbcontext = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ReadOnlyDatabaseContext))!;
        var writeReadcontext = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(WriteReadDatabaseContext))!;
        services.Remove(readOnlyDbcontext);
        services.Remove(writeReadcontext);
        var options = services.Where(r => r.ServiceType == typeof(DbContextOptions)
          || r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)).ToArray();
        foreach(var option in options)
        {
            services.Remove(option);
        }

        /* The options lambda is executed every time a request is made for an AppDbContext, instead of just once on app startup.
         * DON'T use Guid.NewGuid().ToString() inside UseInMemoryDatabase(dbName) method!! */
        var dbName = Guid.NewGuid().ToString();
        services.AddDbContextFactory<ReadOnlyDatabaseContext>(options =>
            options.UseInMemoryDatabase(dbName));
        services.AddDbContextFactory<WriteReadDatabaseContext>(options =>
            options.UseInMemoryDatabase(dbName));
    }
}
