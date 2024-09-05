using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using MediatR;

namespace GameShop.Application.Commands.DeleteGameConsole;

public record DeleteGameConsoleCommand(int GameConsoleId) : IRequest<IResult<DeleteGameConsoleResponse>>;
