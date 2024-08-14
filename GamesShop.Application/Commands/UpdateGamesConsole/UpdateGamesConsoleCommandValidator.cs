using FluentValidation;

namespace GamesShop.Application.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleCommandValidator : AbstractValidator<UpdateGamesConsoleCommand>
{
    public UpdateGamesConsoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive integer.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(100).WithMessage("Manufacturer must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}
