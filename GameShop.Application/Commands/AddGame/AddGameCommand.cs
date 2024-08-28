using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.AddGame;

public record AddGameCommand(
    string Name,
    string Publisher,
    int GamesConsoleId,
    double Price) : IRequest<IResult<AddGameResponse>>;
