using FluentValidation.TestHelper;
using GameShop.Application.Commands.AddGameConsole;

namespace GameShop.Application.UnitTests.Commands.AddGameConsole;

public sealed class AddGameConsoleCommandValidatorTests
{
    private readonly AddGameConsoleCommandValidator _validator;

    public AddGameConsoleCommandValidatorTests()
    {
        _validator = new AddGameConsoleCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new AddGameConsoleCommand(
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
        var command = new AddGameConsoleCommand(
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
        var command = new AddGameConsoleCommand(
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
        var command = new AddGameConsoleCommand(
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
        var command = new AddGameConsoleCommand(
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
        var command = new AddGameConsoleCommand(
            Name: "Valid Name",
            Manufacturer: "Valid Manufacturer",
            Price: 100.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
