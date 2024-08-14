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
            return await mediator.Send(new DeleteGamesConsoleCommand(GamesConsoleId), cancellationToken);
        })
        .WithOpenApi()
        .WithName(nameof(DeleteGamesConsoleModule))
        .WithTags(nameof(GamesConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent);
    }
}
