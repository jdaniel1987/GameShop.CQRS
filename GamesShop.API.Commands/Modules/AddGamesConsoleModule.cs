using Carter;
using GamesShop.Application.Commands.AddGamesConsole;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class AddGamesConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/AddGamesConsole", async (IMediator mediator, AddGamesConsoleCommand command, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Results.Created() : Results.BadRequest(result.Error);
        })
        .WithOpenApi()
        .WithName(nameof(AddGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }
}
