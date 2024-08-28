using Carter;
using GamesShop.Application.Queries.GetGamesByName;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetGamesByNameConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesByName/{GameName}", async (string gameName, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetGamesByNameQuery(gameName));

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all games which name contains the given string";
            operation.Description = "Gets all games which name contains the given string from system.";
            return operation;
        })
        .WithName(nameof(GetGamesByNameConsolesModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
