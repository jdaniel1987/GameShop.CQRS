namespace GameShop.Domain.Services;

public interface IEmailSender
{
    Task SendNotification(string email, string subject, string body);
}
