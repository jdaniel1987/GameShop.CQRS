using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Write.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.AddGame;

public class AddGameHandler(
    IGameWriteRepository gameWriteRepository,
    IGameConsoleWriteRepository gameConsoleWriteRepository,
    IPublisher publisher) : IRequestHandler<AddGameCommand, IResult<AddGameResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;
    private readonly IPublisher _mediatorPublisher = publisher;

    public async Task<IResult<AddGameResponse>> Handle(AddGameCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<AddGameResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }

        var game = request.ToDomain();
        await _gameWriteRepository.AddGame(game, cancellationToken);

        var gameCreatedEvent = game.ToEvent();
        await _mediatorPublisher.Publish(gameCreatedEvent, cancellationToken);

        return Result.Success(game.ToAddGameCommandResponse());
    }
}
