using MediatR;

namespace GamesShop.Application.Events.GameCreated;

public record GameCreatedEvent(
    string GameName,
    string Publisher,
    double PriceUSD,
    double PriceEUR,
    DateTime CreationDate) : INotification;
