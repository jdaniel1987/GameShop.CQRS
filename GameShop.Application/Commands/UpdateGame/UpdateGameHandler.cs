using GameShop.Domain.Repositories;
using MediatR;
using GameShop.Application.Extensions;
using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;

namespace GameShop.Application.Commands.UpdateGame;

public class UpdateGameHandler(
    IGameRepository gameRepository,
    IGameConsoleRepository gameConsoleRepository) : IRequestHandler<UpdateGameCommand, IResult<UpdateGameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;

    public async Task<IResult<UpdateGameResponse>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if (gameConsole is null)
        {
            return Result.Failure<UpdateGameResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }
        var game = request.ToDomain();
        await _gameRepository.UpdateGame(game, cancellationToken);

        return Result.Success(game.ToUpdateGameCommandResponse());
    }
}
