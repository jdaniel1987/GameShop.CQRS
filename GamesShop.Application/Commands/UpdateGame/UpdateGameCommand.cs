using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.UpdateGame;

public record UpdateGameCommand(
    int Id,
    string Name,
    string Publisher,
    int GamesConsoleId,
    double Price) : IRequest<IResult>;
