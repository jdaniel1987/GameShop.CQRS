using FluentValidation.TestHelper;
using GamesShop.Application.Commands.AddGamesConsole;

namespace GamesShop.Application.UnitTests.Commands.AddGamesConsole;

public class AddGamesConsoleCommandValidatorTests
{
    private readonly AddGamesConsoleCommandValidator _validator;

    public AddGamesConsoleCommandValidatorTests()
    {
        _validator = new AddGamesConsoleCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: "",
            Manufacturer: "Valid Manufacturer",
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Exceeds_MaxLength()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: new string('A', 101), // 101 characters
            Manufacturer: "Valid Manufacturer",
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Manufacturer_Is_Empty()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: "Valid Name",
            Manufacturer: "",
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Manufacturer)
            .WithErrorMessage("Manufacturer is required.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Manufacturer_Exceeds_MaxLength()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: "Valid Name",
            Manufacturer: new string('B', 101), // 101 characters
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Manufacturer)
            .WithErrorMessage("Manufacturer must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Price_Is_Negative()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: "Valid Name",
            Manufacturer: "Valid Manufacturer",
            Price: -1.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Price)
            .WithErrorMessage("Price must be greater than or equal to 0.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_Command()
    {
        // Arrange
        var command = new AddGamesConsoleCommand(
            Name: "Valid Name",
            Manufacturer: "Valid Manufacturer",
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
