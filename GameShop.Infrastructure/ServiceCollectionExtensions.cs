using GameShop.Domain.Repositories;
using GameShop.Domain.Services;
using GameShop.Infrastructure.Data;
using GameShop.Infrastructure.EmailSender;
using GameShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GameShop.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var readOnlyConnectionString = configuration.GetConnectionString("ReadOnlyDB");
        var writeReadConnectionString = configuration.GetConnectionString("WriteReadDB");

        services.AddDbContextFactory<ReadOnlyDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(readOnlyConnectionString));
        services.AddDbContextFactory<WriteReadDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(writeReadConnectionString));

        services.AddTransient<IGameRepository, GameRepository>();
        services.AddTransient<IGameConsoleRepository, GameConsoleRepository>();

        services.AddTransient<IEmailSender, FakeEmailSender>();

        return services;
    }
}
