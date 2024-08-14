using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult>;
