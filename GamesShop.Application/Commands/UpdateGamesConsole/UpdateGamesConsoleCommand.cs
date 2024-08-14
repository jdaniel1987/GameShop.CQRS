using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.UpdateGamesConsole;

public record UpdateGamesConsoleCommand(
    int Id,
    string Name,
    string Manufacturer,
    double Price) : IRequest<IResult>;
