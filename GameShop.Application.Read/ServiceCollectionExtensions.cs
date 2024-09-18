using FluentValidation;
using GameShop.Application.Read.Queries.GetGamesForConsole;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameShop.Application.Read;

public static class ServiceCollectionExtensions
{
    public static void AddReadApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<GetGamesForConsoleQueryValidator>();
        services.AddFluentValidation([typeof(GetGamesForConsoleQueryValidator).Assembly]);
    }
}
