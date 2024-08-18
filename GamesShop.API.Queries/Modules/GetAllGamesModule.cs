using Carter;
using GamesShop.Application.Queries.GetAllGames;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetAllGamesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Games", async (IMediator mediator) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(new GetAllGamesQuery()));
        })
        .WithOpenApi()
        .WithName(nameof(GetAllGamesModule))
        .WithTags(nameof(Game));
    }
}
