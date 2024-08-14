using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.AddGame;

public class AddGameHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository gamesConsoleRepository,
    IPublisher publisher) : IRequestHandler<AddGameCommand, IResult>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;
    private readonly IPublisher _mediatorPublisher = publisher;

    public async Task<IResult> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
            if (gamesConsole is null)
            {
                return Results.NotFound($"Games Console with ID: {request.GamesConsoleId} not found.");
            }
            var game = request.ToDomain();
            await _gameRepository.AddGame(game, cancellationToken);

            var gameCreatedEvent = game.ToEvent();
            await _mediatorPublisher.Publish(gameCreatedEvent, cancellationToken);

            return Results.Created($"api/Games/{game.Id}", new { game.Id, game.Name });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Results.Problem("An error occurred while adding the game.", statusCode: 500);
        }
    }
}
