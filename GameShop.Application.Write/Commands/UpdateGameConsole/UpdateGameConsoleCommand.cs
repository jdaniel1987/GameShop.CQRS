using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Write.Commands.UpdateGameConsole;

public record UpdateGameConsoleCommand(
    int Id,
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult<UpdateGameConsoleCommandResponse>>;
