using GamesShop.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using MediatR;
using GamesShop.Domain.Entities;
using GamesShop.Application.Extensions;

namespace GamesShop.Application.Commands.UpdateGame;

public class UpdateGameHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<UpdateGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
        if (gamesConsole is null)
        {
            return Results.NotFound($"Games Console with ID: {request.GamesConsoleId} not found.");
        }
        var game = request.ToDomainWithGamesConsole();
        await _gameRepository.UpdateGame(game, cancellationToken);

        return Results.Ok();
    }
}
