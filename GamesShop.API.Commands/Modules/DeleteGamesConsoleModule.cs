using Carter;
using GamesShop.Application.Commands.DeleteGamesConsole;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class DeleteGamesConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteGamesConsole/{GamesConsoleId:int}", async (IMediator mediator, int GamesConsoleId, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteGamesConsoleCommand(GamesConsoleId), cancellationToken);

            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Deletes a game";
            operation.Description = "Deletes a games console entry from the system.";
            return operation;
        })
        .WithName(nameof(DeleteGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
