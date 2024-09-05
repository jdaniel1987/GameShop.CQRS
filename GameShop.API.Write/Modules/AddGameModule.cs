using Carter;
using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Extensions;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Write.Modules;

public class AddGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/AddGame", async (IMediator mediator, AddGameRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Results.Created() :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Adds a new game";
            operation.Description = "Creates a new game entry in the system.";
            return operation;
        })
        .WithName(nameof(AddGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
