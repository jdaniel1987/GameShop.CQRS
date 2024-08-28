using GameShop.Domain.Repositories;
using MediatR;
using GameShop.Application.Extensions;
using CSharpFunctionalExtensions;

namespace GameShop.Application.Commands.UpdateGame;

public class UpdateGameHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<UpdateGameCommand, IResult<UpdateGameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult<UpdateGameResponse>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
        if (gamesConsole is null)
        {
            return Result.Failure<UpdateGameResponse>($"Games Console with ID: {request.GamesConsoleId} not found.");
        }
        var game = request.ToDomain();
        await _gameRepository.UpdateGame(game, cancellationToken);

        return Result.Success(game.ToUpdateGameResponse());
    }
}
