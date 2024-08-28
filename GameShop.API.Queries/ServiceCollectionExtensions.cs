using Carter;
using GameShop.Application;
using GameShop.Infrastructure;

namespace GameShop.Queries.API;

public static class ServiceCollectionExtensions
{
    public static void AddQueriesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
