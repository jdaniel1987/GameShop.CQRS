using GameShop.Domain.Repositories;
using MediatR;
using CSharpFunctionalExtensions;
using GameShop.Application.Write.Mappers;

namespace GameShop.Application.Write.Commands.UpdateGame;

public class UpdateGameHandler(
    IGameWriteRepository gameWriteRepository,
    IGameConsoleWriteRepository gameConsoleWriteRepository) : IRequestHandler<UpdateGameCommand, IResult<UpdateGameCommandResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<UpdateGameCommandResponse>> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(command.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<UpdateGameCommandResponse>($"Games Console with ID: {command.GameConsoleId} not found.");
        }
        var game = command.ToDomain();
        await _gameWriteRepository.UpdateGame(game, cancellationToken);

        return Result.Success(game.ToUpdateGameCommandResponse());
    }
}
