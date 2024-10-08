using CSharpFunctionalExtensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGameConsole;

public class DeleteGameConsoleHandler(
    IGameConsoleWriteRepository gameConsoleWriteRepository)
    : IRequestHandler<DeleteGameConsoleCommand, IResult<DeleteGameConsoleCommandResponse>>
{
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<DeleteGameConsoleCommandResponse>> Handle(DeleteGameConsoleCommand command, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(command.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<DeleteGameConsoleCommandResponse>($"Games Console with ID: {command.GameConsoleId} not found.");
        }
        await _gameConsoleWriteRepository.DeleteGameConsole(gameConsole, cancellationToken);

        return Result.Success(new DeleteGameConsoleCommandResponse());
    }
}
