using GameShop.Domain.Repositories;
using GameShop.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GameShop.Infrastructure.Write.Data;
using GameShop.Infrastructure.Write.EmailSender;
using GameShop.Infrastructure.Write.Repositories;

namespace GameShop.Infrastructure.Write;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWriteInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var writeReadConnectionString = configuration.GetConnectionString("WriteReadDB");

        services.AddDbContextFactory<WriteReadDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(writeReadConnectionString));

        services.AddTransient<IGameWriteRepository, GameWriteRepository>();
        services.AddTransient<IGameConsoleWriteRepository, GameConsoleWriteRepository>();

        services.AddTransient<IEmailSender, FakeEmailSender>();

        return services;
    }
}
