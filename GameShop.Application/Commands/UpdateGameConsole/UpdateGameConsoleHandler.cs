using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.UpdateGameConsole;

public class UpdateGameConsoleHandler(
    IGameConsoleRepository gameConsoleRepository) : IRequestHandler<UpdateGameConsoleCommand, IResult<UpdateGameConsoleCommandResponse>>
{
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;

    public async Task<IResult<UpdateGameConsoleCommandResponse>> Handle(UpdateGameConsoleCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = request.ToDomain();
        await _gameConsoleRepository.UpdateGameConsole(gameConsole, cancellationToken);

        return Result.Success(gameConsole.ToUpdateGameConsoleCommandResponse());
    }
}
