using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.DeleteGameConsole;

public record DeleteGameConsoleCommand(int GameConsoleId) : IRequest<IResult<DeleteGameConsoleResponse>>;
