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
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithOpenApi()
        .WithName(nameof(UpdateGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent);
    }
}
