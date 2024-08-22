using Carter;
using GamesShop.Application.Commands.DeleteGame;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class DeleteGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteGame/{GameId:int}", async (int GameId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteGameCommand(GameId), cancellationToken);

            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithOpenApi()
        .WithName(nameof(DeleteGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent);
    }
}
