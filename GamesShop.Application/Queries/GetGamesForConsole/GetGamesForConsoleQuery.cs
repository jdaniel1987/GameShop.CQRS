using CSharpFunctionalExtensions;
using MediatR;

namespace GamesShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleQuery(int GamesConsoleId) : IRequest<IResult<GetGamesForConsoleResponse>>;
