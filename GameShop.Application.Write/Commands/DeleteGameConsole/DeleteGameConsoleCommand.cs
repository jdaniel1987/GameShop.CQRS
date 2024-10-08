using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGameConsole;

public record DeleteGameConsoleCommand(int GameConsoleId) : IRequest<IResult<DeleteGameConsoleCommandResponse>>;
