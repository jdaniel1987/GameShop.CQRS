using GamesShop.Domain.Entities;
using MediatR;

namespace GamesShop.Application.Events.GameCreated;

public record GameCreatedEvent(
    Game Game,
    DateTime CreationDate) : INotification;
