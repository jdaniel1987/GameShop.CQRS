using Carter;
using GameShop.Application.Extensions;
using GameShop.Application.Queries.GetGamesByName;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Read.Modules;

public class GetGamesByNameConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesByName/{GameName}", async (IMediator mediator, string gameName) =>
        {
            var result = await mediator.Send(new GetGamesByNameQuery(gameName));

            return result.IsSuccess ?
                Results.Ok(result.Value.ToGetGamesByNameResponse()) :
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
