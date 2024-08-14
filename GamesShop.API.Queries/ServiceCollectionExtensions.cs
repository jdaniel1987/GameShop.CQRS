using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using GamesShop.Application;
using GamesShop.Application.Queries.GetGamesForConsole;
using GamesShop.Infrastructure;

namespace GamesShop.Queries.API;

public static class ServiceCollectionExtensions
{
    public static void AddQueriesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<GetGamesForConsoleQueryValidator>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
