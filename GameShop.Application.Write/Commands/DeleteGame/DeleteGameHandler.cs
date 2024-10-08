using CSharpFunctionalExtensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGame;

public class DeleteGameHandler(IGameWriteRepository gameWriteRepository) : IRequestHandler<DeleteGameCommand, IResult<DeleteGameCommandResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;

    public async Task<IResult<DeleteGameCommandResponse>> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
    {
        var game = await _gameWriteRepository.GetGame(command.GameId, cancellationToken);
        if(game is null)
        {
            return Result.Failure<DeleteGameCommandResponse>($"Game with ID: {command.GameId} not found.");
        }
        await _gameWriteRepository.DeleteGame(game, cancellationToken);

        return Result.Success(new DeleteGameCommandResponse());
    }
}
