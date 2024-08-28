using AutoFixture.Xunit2;
using GameShop.Infrastructure.EmailSender;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameShop.Infrastructure.UnitTests.EmailSender;

public class FakeEmailSenderTests
{
    public sealed class SendNotification
    {
        private readonly FakeEmailSender _fakeEmailSender;
        private readonly Mock<ILogger<FakeEmailSender>> _mockLogger;

        public SendNotification()
        {
            _mockLogger = new Mock<ILogger<FakeEmailSender>>();
            _fakeEmailSender = new FakeEmailSender(_mockLogger.Object);
        }

        [Theory, AutoData] // AutoData autogenerates test param values
        public async Task Should_log_info(
            string email,
            string subject,
            string body)
        {
            // Arrange

            // Act
            await _fakeEmailSender.SendNotification(email, subject, body);

            // Assert
            // Need to import ILogger.Moq to the project in order to use VerifyLog, as regular Verify is not working because LogInformation is an static method
            _mockLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", email), Times.Once);
            _mockLogger.VerifyLog(mock => mock.LogInformation("Body: {Body}", body), Times.Once);
        }
    }
}
