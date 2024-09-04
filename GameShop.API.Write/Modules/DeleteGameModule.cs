using Carter;
using GameShop.Application.Commands.DeleteGame;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Write.Modules;

public class DeleteGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteGame/{GameId:int}", async (IMediator mediator, int GameId, CancellationToken cancellationToken) =>
        {
            var command = new DeleteGameCommand(GameId);
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Results.NoContent() :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Delete a game";
            operation.Description = "Deletes a game entry from system.";
            return operation;
        })
        .WithName(nameof(DeleteGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
