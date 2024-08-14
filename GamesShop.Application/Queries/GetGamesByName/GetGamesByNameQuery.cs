using MediatR;

namespace GamesShop.Application.Queries.GetGamesByName;

public record GetGamesByNameQuery(string GameName) : IRequest<GetGamesByNameResponse>;
