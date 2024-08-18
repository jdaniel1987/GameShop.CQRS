using CSharpFunctionalExtensions;
using MediatR;

namespace GamesShop.Application.Commands.DeleteGamesConsole;

public record DeleteGamesConsoleCommand(int GamesConsoleId) : IRequest<IResult<DeleteGamesConsoleResponse>>;
