using FluentValidation;

namespace GameShop.Application.Queries.GetGamesByName;

public class GetGamesByNameQueryValidator : AbstractValidator<GetGamesByNameQuery>
{
    public GetGamesByNameQueryValidator()
    {
        RuleFor(x => x.GameName)
            .NotEmpty().WithMessage("GameName is required.")
            .MaximumLength(100).WithMessage("GameName must not exceed 100 characters.");
    }
}
