using GameShop.Application.Queries.GetGamesForConsole;
using FluentValidation.TestHelper;

namespace GameShop.Application.UnitTests.Queries.GetGamesForConsole;

public class GetGamesForConsoleQueryValidatorTests
{
    private readonly GetGamesForConsoleQueryValidator _validator;

    public GetGamesForConsoleQueryValidatorTests()
    {
        _validator = new GetGamesForConsoleQueryValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GamesConsoleId_Is_Less_Than_Or_Equal_To_Zero()
    {
        // Arrange
        var query = new GetGamesForConsoleQuery(GamesConsoleId: 0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.GamesConsoleId)
            .WithErrorMessage("GamesConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GamesConsoleId()
    {
        // Arrange
        var query = new GetGamesForConsoleQuery(GamesConsoleId: 1);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
