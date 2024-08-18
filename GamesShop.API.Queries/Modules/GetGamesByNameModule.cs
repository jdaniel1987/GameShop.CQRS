using Carter;
using GamesShop.Application.Queries.GetGamesByName;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetGamesByNameConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesByName/{GameName}", async (string GameName, IMediator mediator) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(new GetGamesByNameQuery(GameName)));
        })
        .WithOpenApi()
        .WithName(nameof(GetGamesByNameConsolesModule))
        .WithTags(nameof(Game));
    }
}
