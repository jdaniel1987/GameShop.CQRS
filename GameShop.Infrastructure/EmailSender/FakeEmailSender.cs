using GameShop.Domain.Services;
using Microsoft.Extensions.Logging;

namespace GameShop.Infrastructure.EmailSender;

public class FakeEmailSender(ILogger<FakeEmailSender> logger) : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger = logger;

    public async Task SendNotification(string email, string subject, string body)
    {
        _logger.LogInformation("Sent mail to {Email} ", email);
        _logger.LogInformation("Subject: {Subject}", subject);
        _logger.LogInformation("Body: {Body}", body);

        await Task.Delay(1000);
    }
}
