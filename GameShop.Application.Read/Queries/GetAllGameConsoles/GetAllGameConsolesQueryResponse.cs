﻿namespace GameShop.Application.Read.Queries.GetAllGameConsoles;

public record GetAllGameConsolesQueryResponse(IReadOnlyCollection<GetAllGameConsolesQueryResponseItem> GameConsoles);