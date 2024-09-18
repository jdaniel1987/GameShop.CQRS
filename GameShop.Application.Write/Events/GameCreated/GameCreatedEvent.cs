using MediatR;

namespace GameShop.Application.Write.Events.GameCreated;

public record GameCreatedEvent(
    string GameName,
    string Publisher,
    double PriceUSD,
    double PriceEUR,
    DateTime CreationDate) : INotification;
