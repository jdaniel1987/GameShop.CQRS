using Carter;
using GamesShop.Application.Queries.GetAllGamesConsoles;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Queries.Modules;

public class GetAllGamesConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GamesConsoles", async (IMediator mediator) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(new GetAllGamesConsolesQuery()));
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
