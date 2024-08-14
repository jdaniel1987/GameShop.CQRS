using GamesShop.Domain.Services;
using GamesShop.Domain.ValueObjects;
using MediatR;

namespace GamesShop.Application.Events.GameCreated;

public class GameCreatedEventHandler(
    IEmailSender emailSender) : INotificationHandler<GameCreatedEvent>
{
    private readonly IEmailSender _emailSender = emailSender;

    public async Task Handle(GameCreatedEvent notification, CancellationToken cancellationToken)
    {
        var releaseDate = notification.CreationDate;
        var gameName = notification.Game.Name;
        var gamePublisher = notification.Game.Publisher;
        var gamePriceUsd = notification.Game.Price;
        PriceEuros gamePriceEuro = notification.Game.Price;

        await _emailSender.SendNotification(
            "random@email.com",
            $"{releaseDate:dd-MM-yyyy HH:mm} - New Game from {gamePublisher}",
            $"Product {gameName} with price USD {gamePriceUsd} / EUR {gamePriceEuro}"); // This will print "1234" value because I overrode ToString on ValueObjects,
                                                                                        // Otherwise, it would print "PriceEuros { Value = 1234 }"
    }
}
