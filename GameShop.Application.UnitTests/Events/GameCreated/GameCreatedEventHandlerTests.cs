using GameShop.Application.Events.GameCreated;
using GameShop.Domain.Services;

namespace GameShop.Application.UnitTests.Events.GameCreated;

public sealed class GameCreatedEventHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldSendEmailNotification_WhenGameCreated(
        [Frozen] Mock<IEmailSender> emailSenderMock,
        GameCreatedEvent notification)
    {
        // Arrange
        var handler = new GameCreatedHandler(emailSenderMock.Object);

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        emailSenderMock.Verify(s => s.SendNotification(
            "random@email.com",
            $"{notification.CreationDate:dd-MM-yyyy HH:mm} - New Game from {notification.Publisher}",
            $"Product {notification.GameName} with price USD {notification.PriceUSD} / EUR {notification.PriceEUR}"
            ), Times.Once);
    }
}
