using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGameConsole;

public record DeleteGameConsoleCommand(int GameConsoleId) : IRequest<IResult<DeleteGameConsoleResponse>>;
