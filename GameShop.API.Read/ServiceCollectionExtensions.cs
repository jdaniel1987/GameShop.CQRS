using Carter;
using GameShop.Application;
using GameShop.Infrastructure;

namespace GameShop.API.Read;

public static class ServiceCollectionExtensions
{
    public static void AddReadApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
