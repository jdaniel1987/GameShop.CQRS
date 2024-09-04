using Carter;
using GameShop.Application.Commands.DeleteGameConsole;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Write.Modules;

public class DeleteGameConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteGameConsole/{GameConsoleId:int}", async (IMediator mediator, int GameConsoleId, CancellationToken cancellationToken) =>
        {
            var command = new DeleteGameConsoleCommand(GameConsoleId);
            var result = await mediator.Send(command, cancellationToken);

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
        .WithName(nameof(DeleteGameConsoleModule))
        .WithTags(nameof(GameConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
