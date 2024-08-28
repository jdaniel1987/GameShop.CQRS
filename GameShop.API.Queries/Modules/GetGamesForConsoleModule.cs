using Carter;
using GameShop.Application.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Queries.Modules;

public class GetGamesForConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesForConsole/{GamesConsoleId:int}", async (IMediator mediator, int gamesConsoleId) =>
        {;
            var result = await mediator.Send(new GetGamesForConsoleQuery(gamesConsoleId));

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all games for given games console ID";
            operation.Description = "Gets all games for given games console ID from system.";
            return operation;
        })
        .WithName(nameof(GetGamesForConsoleModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
