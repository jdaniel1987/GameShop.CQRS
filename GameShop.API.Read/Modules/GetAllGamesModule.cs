using Carter;
using GameShop.Application.Extensions;
using GameShop.Application.Queries.GetAllGames;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Read.Modules;

public class GetAllGamesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Games", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllGamesQuery());

            return result.IsSuccess ?
                Results.Ok(result.Value.ToGetAllGamesResponse()) :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all games";
            operation.Description = "Gets all games from system.";
            return operation;
        })
        .WithName(nameof(GetAllGamesModule))
        .WithTags(nameof(Game))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
