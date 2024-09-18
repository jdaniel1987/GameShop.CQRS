using CSharpFunctionalExtensions;
using GameShop.Application.Write.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.UpdateGameConsole;

public class UpdateGameConsoleHandler(
    IGameConsoleWriteRepository gameConsoleWriteRepository) : IRequestHandler<UpdateGameConsoleCommand, IResult<UpdateGameConsoleCommandResponse>>
{
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<UpdateGameConsoleCommandResponse>> Handle(UpdateGameConsoleCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = request.ToDomain();
        await _gameConsoleWriteRepository.UpdateGameConsole(gameConsole, cancellationToken);

        return Result.Success(gameConsole.ToUpdateGameConsoleCommandResponse());
    }
}
