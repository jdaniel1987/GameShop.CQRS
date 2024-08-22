using Carter;
using GamesShop.Application.Commands.DeleteGamesConsole;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class DeleteGamesConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/DeleteGamesConsole/{GamesConsoleId:int}", async (int GamesConsoleId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteGamesConsoleCommand(GamesConsoleId), cancellationToken);

            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithOpenApi()
        .WithName(nameof(DeleteGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent);
    }
}
