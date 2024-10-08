using CSharpFunctionalExtensions;
using GameShop.Application.Write.Mappers;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.AddGame;

public class AddGameHandler(
    IGameWriteRepository gameWriteRepository,
    IGameConsoleWriteRepository gameConsoleWriteRepository,
    IPublisher publisher) : IRequestHandler<AddGameCommand, IResult<AddGameCommandResponse>>
{
    private readonly IGameWriteRepository _gameWriteRepository = gameWriteRepository;
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;
    private readonly IPublisher _mediatorPublisher = publisher;

    public async Task<IResult<AddGameCommandResponse>> Handle(AddGameCommand command, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(command.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<AddGameCommandResponse>($"Games Console with ID: {command.GameConsoleId} not found.");
        }

        var game = command.ToDomain();
        await _gameWriteRepository.AddGame(game, cancellationToken);

        var gameCreatedEvent = game.ToEvent();
        await _mediatorPublisher.Publish(gameCreatedEvent, cancellationToken);

        return Result.Success(game.ToAddGameCommandResponse());
    }
}
