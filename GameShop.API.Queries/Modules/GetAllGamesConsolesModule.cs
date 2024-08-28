using Carter;
using GameShop.Application.Queries.GetAllGamesConsoles;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Queries.Modules;

public class GetAllGamesConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesConsoles", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllGamesConsolesQuery());

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all games consoles";
            operation.Description = "Gets all games consoles from system.";
            return operation;
        })
        .WithName(nameof(GetAllGamesConsolesModule))
        .WithTags(nameof(GamesConsole))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
