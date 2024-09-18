using GameShop.Domain.Repositories;
using MediatR;
using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Write.Extensions;

namespace GameShop.Application.Write.Commands.UpdateGame;

public class UpdateGameHandler(
    IGameWriteRepository gameWriteRepository,
    IGameConsoleWriteRepository gameConsoleWriteRepository) : IRequestHandler<UpdateGameCommand, IResult<UpdateGameResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<UpdateGameResponse>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<UpdateGameResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }
        var game = request.ToDomain();
        await _gameWriteRepository.UpdateGame(game, cancellationToken);

        return Result.Success(game.ToUpdateGameCommandResponse());
    }
}
