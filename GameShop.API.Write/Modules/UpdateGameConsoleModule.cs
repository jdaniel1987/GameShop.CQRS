using Carter;
using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Extensions;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Write.Modules;

public class UpdateGameConsoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/UpdateGameConsole", async (IMediator mediator, UpdateGameConsoleRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.ToCommand();
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
        .WithName(nameof(UpdateGameConsoleModule))
        .WithTags(nameof(GameConsole))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
