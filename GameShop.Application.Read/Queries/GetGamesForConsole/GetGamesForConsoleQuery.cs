using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Read.Queries.GetGamesForConsole;

public record GetGamesForConsoleQuery(int GameConsoleId) : IRequest<IResult<GetGamesForConsoleQueryResponse>>;
