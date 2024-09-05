using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.DeleteGame;

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

        return Result.Success(new DeleteGameResponse());
    }
}
