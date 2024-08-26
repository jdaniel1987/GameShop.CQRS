using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Commands.AddGame;

public class AddGameHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository gamesConsoleRepository,
    IPublisher publisher) : IRequestHandler<AddGameCommand, IResult<AddGameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;
    private readonly IPublisher _mediatorPublisher = publisher;

    public async Task<IResult<AddGameResponse>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
        if (gamesConsole is null)
        {
            return Result.Failure<AddGameResponse>($"Games Console with ID: {request.GamesConsoleId} not found.");
        }

        var game = request.ToDomain();
        await _gameRepository.AddGame(game, cancellationToken);

        var gameCreatedEvent = game.ToEvent();
        await _mediatorPublisher.Publish(gameCreatedEvent, cancellationToken);

        return Result.Success(game.ToAddGameResponse());
    }
}
