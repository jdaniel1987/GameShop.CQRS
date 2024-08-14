using GamesShop.Domain.Repositories;
using GamesShop.Domain.Services;
using GamesShop.Infrastructure.Data;
using GamesShop.Infrastructure.EmailSender;
using GamesShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GamesShop.Infrastructure;

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
        services.AddTransient<IGamesConsoleRepository, GamesConsoleRepository>();

        services.AddTransient<IEmailSender, FakeEmailSender>();

        return services;
    }
}
