using Carter;
using GameShop.Application.Read;
using GameShop.Infrastructure.Read;

namespace GameShop.API.Read;

public static class ServiceCollectionExtensions
{
    public static void AddReadApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReadInfrastructure(configuration);
        services.AddReadApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
