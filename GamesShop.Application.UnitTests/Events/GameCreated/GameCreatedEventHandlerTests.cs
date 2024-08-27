using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using GamesShop.Application.Events.GameCreated;
using Moq;
using GamesShop.Domain.Services;
using GamesShop.Domain.ValueObjects;

namespace GamesShop.Application.UnitTests.Events.GameCreated;

public class GameCreatedEventHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldSendEmailNotification_WhenGameCreated(
        Mock<IEmailSender> emailSenderMock,
        GameCreatedEvent notification)
    {
        // Arrange
        var handler = new GameCreatedEventHandler(emailSenderMock.Object);

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        emailSenderMock.Verify(s => s.SendNotification(
            "random@email.com",
            $"{notification.CreationDate.ToString("dd-MM-yyyy HH:mm")} - New Game from {notification.Publisher}",
            $"Product {notification.GameName} with price USD {notification.PriceUSD} / EUR {notification.PriceEUR}"
            ), Times.Once);
    }
}
