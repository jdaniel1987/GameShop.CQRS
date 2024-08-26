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

            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Updates a games console";
            operation.Description = "Updates a games console entry in the system.";
            return operation;
        })
        .WithName(nameof(UpdateGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
