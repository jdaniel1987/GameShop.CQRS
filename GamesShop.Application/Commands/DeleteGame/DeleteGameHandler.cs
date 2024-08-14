using GamesShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.DeleteGame;

public class DeleteGameHandler(IGameRepository gameRepository) : IRequestHandler<DeleteGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<IResult> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGame(request.GameId, cancellationToken);
        if (game is null)
        {
            return Results.NotFound($"Game with ID: {request.GameId} not found.");
        }
        await _gameRepository.DeleteGame(game, cancellationToken);

        return Results.Ok();
    }
}
