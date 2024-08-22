using Carter;
using GamesShop.Application.Commands.AddGame;
using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.API.Commands.Modules;

public class AddGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/AddGame", async (IMediator mediator, AddGameCommand command, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Results.Created() : Results.BadRequest(result.Error);
        })
        .WithOpenApi()
        .WithName(nameof(AddGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created);
    }
}
