using CSharpFunctionalExtensions;
using GameShop.Application.Write.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.AddGameConsole;

public class AddGameConsoleHandler(
    IGameConsoleWriteRepository gameConsoleWriteRepository) : IRequestHandler<AddGameConsoleCommand, IResult<AddGameConsoleCommandResponse>>
{
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<AddGameConsoleCommandResponse>> Handle(AddGameConsoleCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = request.ToDomain();
        await _gameConsoleWriteRepository.AddGameConsole(gameConsole, cancellationToken);

        return Result.Success(gameConsole.ToAddGameConsoleCommandResponse());
    }
}
