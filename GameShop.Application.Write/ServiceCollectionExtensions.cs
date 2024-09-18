using FluentValidation;
using GameShop.Application.Write.Commands.AddGame;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameShop.Application.Write;

public static class ServiceCollectionExtensions
{
    public static void AddWriteApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<AddGameCommandValidator>();
        services.AddFluentValidation([typeof(AddGameCommandValidator).Assembly]);
    }
}
