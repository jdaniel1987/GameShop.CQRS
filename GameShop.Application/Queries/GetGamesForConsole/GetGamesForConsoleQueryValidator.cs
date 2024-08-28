using FluentValidation;

namespace GameShop.Application.Queries.GetGamesForConsole;

public class GetGamesForConsoleQueryValidator : AbstractValidator<GetGamesForConsoleQuery>
{
    public GetGamesForConsoleQueryValidator()
    {
        RuleFor(x => x.GamesConsoleId)
            .GreaterThan(0).WithMessage("GamesConsoleId must be a positive integer.");
    }
}
