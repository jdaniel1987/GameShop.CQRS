using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.DeleteGamesConsole;

public record DeleteGamesConsoleCommand(int GamesConsoleId) : IRequest<IResult>;
