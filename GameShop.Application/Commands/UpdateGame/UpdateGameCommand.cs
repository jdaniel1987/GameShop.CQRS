using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.UpdateGame;

public record UpdateGameCommand(
    int Id,
    string Name,
    string Publisher,
    int GameConsoleId,
    double Price) : IRequest<IResult<UpdateGameResponse>>;
