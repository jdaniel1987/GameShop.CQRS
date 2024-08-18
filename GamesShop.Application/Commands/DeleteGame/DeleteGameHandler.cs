using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Commands.DeleteGame;

public class DeleteGameHandler(IGameRepository gameRepository) : IRequestHandler<DeleteGameCommand, IResult<DeleteGameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<IResult<DeleteGameResponse>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetGame(request.GameId, cancellationToken);
        if (game is null)
        {
            return Result.Failure<DeleteGameResponse>($"Game with ID: {request.GameId} not found.");
        }
        await _gameRepository.DeleteGame(game, cancellationToken);

        return Result.Success(game.ToDeleteGameResponse());
    }
}
