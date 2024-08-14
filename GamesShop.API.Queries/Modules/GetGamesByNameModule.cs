using Carter;
using GamesShop.Application.Queries.GetGamesByName;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetGamesByNameConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesByName/{GameName}", (string GameName, IMediator mediator) =>
        {
            return mediator.Send(new GetGamesByNameQuery(GameName));
        })
        .WithOpenApi()
        .WithName(nameof(GetGamesByNameConsolesModule))
        .WithTags(nameof(Game));
    }
}
