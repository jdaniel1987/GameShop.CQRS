using Carter;
using GamesShop.Application.Commands.AddGame;
using GamesShop.Domain.Entities;
using MediatR;
using System.Runtime.CompilerServices;

namespace GamesShop.API.Commands.Modules;

public class AddGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/AddGame", async (IMediator mediator, AddGameCommand command, CancellationToken cancellationToken) =>
        {
            return ResultChecker.CheckResult(await mediator.Send(command, cancellationToken));
        })
        .WithOpenApi()
        .WithName(nameof(AddGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }
}
