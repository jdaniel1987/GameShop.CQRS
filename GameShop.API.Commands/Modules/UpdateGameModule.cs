﻿using Carter;
using GameShop.Application.Commands.UpdateGame;
using GameShop.Domain.Entities;
using MediatR;

namespace GameShop.API.Commands.Modules;

public class UpdateGameModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/UpdateGame", async (IMediator mediator, UpdateGameCommand command, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Updates a game";
            operation.Description = "Updates a game entry in the system.";
            return operation;
        })
        .WithName(nameof(UpdateGameModule))
        .WithTags(nameof(Game))
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}