using GameShop.Domain.Services;
using MediatR;

namespace GameShop.Application.Events.GameCreated;

public class GameCreatedHandler(
    IEmailSender emailSender) : INotificationHandler<GameCreatedEvent>
{
    private readonly IEmailSender _emailSender = emailSender;

    public async Task Handle(GameCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _emailSender.SendNotification(
            "random@email.com",
            $"{notification.CreationDate:dd-MM-yyyy HH:mm} - New Game from {notification.Publisher}",
            $"Product {notification.GameName} with price USD {notification.PriceUSD} / EUR {notification.PriceEUR}"); // This will print "1234" value because I overrode ToString on ValueObjects,
                                                                                                                      // Otherwise, it would print "PriceEuros { Value = 1234 }"
    }
}
