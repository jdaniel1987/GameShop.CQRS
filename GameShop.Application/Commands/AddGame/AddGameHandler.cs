using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.AddGame;

public class AddGameHandler(
    IGameRepository gameRepository,
    IGameConsoleRepository gameConsoleRepository,
    IPublisher publisher) : IRequestHandler<AddGameCommand, IResult<AddGameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;
    private readonly IPublisher _mediatorPublisher = publisher;

    public async Task<IResult<AddGameResponse>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if (gameConsole is null)
        {
            return Result.Failure<AddGameResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }

        var game = request.ToDomain();
        await _gameRepository.AddGame(game, cancellationToken);

        var gameCreatedEvent = game.ToEvent();
        await _mediatorPublisher.Publish(gameCreatedEvent, cancellationToken);

        return Result.Success(game.ToAddGameCommandResponse());
    }
}
