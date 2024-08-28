using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.DeleteGamesConsole;

public record DeleteGamesConsoleCommand(int GamesConsoleId) : IRequest<IResult<DeleteGamesConsoleResponse>>;
