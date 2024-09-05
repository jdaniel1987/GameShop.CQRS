using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.AddGameConsole;

public record AddGameConsoleCommand(
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult<AddGameConsoleCommandResponse>>;
