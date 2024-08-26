using Carter;
using GamesShop.Application;
using GamesShop.Infrastructure;

namespace GamesShop.Queries.API;

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
