using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.AddGame;

public record AddGameCommand(
    string Name,
    string Publisher,
    int GamesConsoleId,
    double Price) : IRequest<IResult>;
