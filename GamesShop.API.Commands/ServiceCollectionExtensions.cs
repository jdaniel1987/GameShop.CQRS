using Carter;
using GamesShop.Application;
using GamesShop.Infrastructure;

namespace GamesShop.Commands.API;

public static class ServiceCollectionExtensions
{
    public static void AddCommandsApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
