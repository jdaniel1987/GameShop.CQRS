using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleQuery(int GameConsoleId) : IRequest<IResult<GetGamesForConsoleQueryResponse>>;
