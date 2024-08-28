using FluentValidation;
using GameShop.Application.Commands.AddGame;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameShop.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<AddGameCommandValidator>();
        services.AddFluentValidation([typeof(AddGameCommandValidator).Assembly]);
    }
}
