using Carter;
using GamesShop.Application.Queries.GetGamesForConsole;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetGamesForConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesForConsole/{GamesConsoleId:int}", async (int GamesConsoleId, IMediator mediator) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(new GetGamesForConsoleQuery(GamesConsoleId)));
        })
        .WithOpenApi()
        .WithName(nameof(GetGamesForConsoleModule))
        .WithTags(nameof(Game));
    }
}
