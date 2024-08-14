using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using GamesShop.Application;
using GamesShop.Application.Commands.AddGame;
using GamesShop.Infrastructure;

namespace GamesShop.Commands.API;

public static class ServiceCollectionExtensions
{
    public static void AddCommandsApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<AddGameCommandValidator>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
