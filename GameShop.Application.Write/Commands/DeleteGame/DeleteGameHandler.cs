using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGame;

public class DeleteGameHandler(IGameWriteRepository gameWriteRepository) : IRequestHandler<DeleteGameCommand, IResult<DeleteGameResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;

    public async Task<IResult<DeleteGameResponse>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameWriteRepository.GetGame(request.GameId, cancellationToken);
        if(game is null)
        {
            return Result.Failure<DeleteGameResponse>($"Game with ID: {request.GameId} not found.");
        }
        await _gameWriteRepository.DeleteGame(game, cancellationToken);

        return Result.Success(new DeleteGameResponse());
    }
}
