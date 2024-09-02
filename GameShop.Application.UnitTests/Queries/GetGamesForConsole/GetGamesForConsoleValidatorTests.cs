using GameShop.Application.Queries.GetGamesForConsole;
using FluentValidation.TestHelper;

namespace GameShop.Application.UnitTests.Queries.GetGamesForConsole;

public sealed class GetGamesForConsoleQueryValidatorTests
{
    private readonly GetGamesForConsoleQueryValidator _validator;

    public GetGamesForConsoleQueryValidatorTests()
    {
        _validator = new GetGamesForConsoleQueryValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameConsoleId_Is_Less_Than_Or_Equal_To_Zero()
    {
        // Arrange
        var query = new GetGamesForConsoleQuery(GameConsoleId: 0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.GameConsoleId)
            .WithErrorMessage("GameConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GameConsoleId()
    {
        // Arrange
        var query = new GetGamesForConsoleQuery(GameConsoleId: 1);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
