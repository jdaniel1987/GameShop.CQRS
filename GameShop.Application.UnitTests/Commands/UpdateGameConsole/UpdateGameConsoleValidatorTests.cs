using FluentValidation.TestHelper;
using GameShop.Application.Commands.UpdateGameConsole;

namespace GameShop.Application.UnitTests.Commands.UpdateGameConsole;

public sealed class UpdateGameConsoleCommandValidatorTests
{
    private readonly UpdateGameConsoleCommandValidator _validator;

    public UpdateGameConsoleCommandValidatorTests()
    {
        _validator = new UpdateGameConsoleCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Id_Is_Zero()
    {
        // Arrange
        var command = new UpdateGameConsoleCommand(Id: 0, Name: "Valid Name", Manufacturer: "Valid Manufacturer", Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Id_Is_Negative()
    {
        // Arrange
        var command = new UpdateGameConsoleCommand(Id: -1, Name: "Valid Name", Manufacturer: "Valid Manufacturer", Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new UpdateGameConsoleCommand(Id: 1, Name: "", Manufacturer: "Valid Manufacturer", Price: 10.0);

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
        var command = new UpdateGameConsoleCommand(Id: 1, Name: new string('A', 101), Manufacturer: "Valid Manufacturer", Price: 10.0);

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
        var command = new UpdateGameConsoleCommand(Id: 1, Name: "Valid Name", Manufacturer: "", Price: 10.0);

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
        var command = new UpdateGameConsoleCommand(Id: 1, Name: "Valid Name", Manufacturer: new string('B', 101), Price: 10.0);

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
        var command = new UpdateGameConsoleCommand(Id: 1, Name: "Valid Name", Manufacturer: "Valid Manufacturer", Price: -1.0);

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
        var command = new UpdateGameConsoleCommand(Id: 1, Name: "Valid Name", Manufacturer: "Valid Manufacturer", Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
