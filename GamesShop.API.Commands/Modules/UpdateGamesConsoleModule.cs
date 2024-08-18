using Carter;
using GamesShop.Application.Commands.UpdateGamesConsole;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class UpdateGamesConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/UpdateGamesConsole", async (IMediator mediator, UpdateGamesConsoleCommand command, CancellationToken cancellationToken) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(command, cancellationToken));
        })
        .WithOpenApi()
        .WithName(nameof(UpdateGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK);
    }
}
