using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.UpdateGamesConsole;

public record UpdateGamesConsoleCommand(
    int Id,
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult<UpdateGamesConsoleResponse>>;
