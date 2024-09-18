using GameShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GameShop.Infrastructure.Read.Data;
using GameShop.Infrastructure.Read.Repositories;

namespace GameShop.Infrastructure.Read;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReadInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var readOnlyConnectionString = configuration.GetConnectionString("ReadOnlyDB");

        services.AddDbContextFactory<ReadOnlyDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(readOnlyConnectionString));

        services.AddTransient<IGameReadRepository, GameReadRepository>();
        services.AddTransient<IGameConsoleReadRepository, GameConsoleReadRepository>();

        return services;
    }
}
