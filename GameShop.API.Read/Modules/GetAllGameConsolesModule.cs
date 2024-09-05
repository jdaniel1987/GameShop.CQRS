using Carter;
using GameShop.Application.Extensions;
using GameShop.Application.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Read.Modules;

public class GetAllGameConsolesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/GameConsoles", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllGameConsolesQuery());

            return result.IsSuccess ?
                Results.Ok(result.Value.ToGetAllGameConsolesResponse()) :
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all games consoles";
            operation.Description = "Gets all games consoles from system.";
            return operation;
        })
        .WithName(nameof(GetAllGameConsolesModule))
        .WithTags(nameof(GameConsole))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
