using Carter;
using GamesShop.Application.Commands.UpdateGame;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class UpdateGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/UpdateGame", async (IMediator mediator, UpdateGameCommand command, CancellationToken cancellationToken) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(command, cancellationToken));
        })
        .WithOpenApi()
        .WithName(nameof(UpdateGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status200OK);
    }
}
