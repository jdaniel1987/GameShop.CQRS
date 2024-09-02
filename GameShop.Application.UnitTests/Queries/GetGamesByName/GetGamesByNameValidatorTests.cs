using GameShop.Application.Queries.GetGamesByName;
using FluentValidation.TestHelper;

namespace GameShop.Application.UnitTests.Queries.GetGamesByName;

public sealed class GetGamesByNameQueryValidatorTests
{
    private readonly GetGamesByNameQueryValidator _validator;

    public GetGamesByNameQueryValidatorTests()
    {
        _validator = new GetGamesByNameQueryValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameName_Is_Empty()
    {
        // Arrange
        var query = new GetGamesByNameQuery(GameName: string.Empty);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.GameName)
            .WithErrorMessage("GameName is required.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameName_Exceeds_MaxLength()
    {
        // Arrange
        var query = new GetGamesByNameQuery(GameName: new string('A', 101));

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.GameName)
            .WithErrorMessage("GameName must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GameName()
    {
        // Arrange
        var query = new GetGamesByNameQuery(GameName: "Valid Game Name");

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
