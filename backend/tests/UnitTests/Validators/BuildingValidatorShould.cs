using BMS.Domain.Entities;
using BMS.Application.DTOs;
using BMS.Application.Validators;

namespace BMS.UnitTests.Validators;

public class BuildingValidatorShould
{
    [Fact]
    public void BuildingValidator_ValidInput_ReturnsValid()
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: 10,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(3)]     // Minimum boundary
    [InlineData(50)]    // Maximum boundary
    public void BuildingValidator_ValidBuildingName_ReturnsValid(int letterCount)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: new string('A', letterCount),
            BuildingAddress: "123 Main St",
            NumberOfUnits: 10,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]     // Empty string should be invalid
    [InlineData(1)]     // Too short
    [InlineData(51)]    // Too long
    public void BuildingValidator_InvalidBuildingName_ReturnsInvalid(int letterCount)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: new string('A', letterCount),
            BuildingAddress: "123 Main St",
            NumberOfUnits: 10,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(10)]     // Minimum boundary
    [InlineData(100)]    // Maximum boundary
    public void BuildingValidator_ValidBuildingAddress_ReturnsValid(int letterCount)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: new string('A', letterCount),
            NumberOfUnits: 10,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]     // Empty string should be invalid
    [InlineData(1)]     // Too short
    [InlineData(101)]   // Too long
    public void BuildingValidator_InvalidBuildingAddress_ReturnsInvalid(int letterCount)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: new string('A', letterCount),
            NumberOfUnits: 10,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(1)]     // Minimum boundary
    [InlineData(10000)] // Maximum boundary
    public void BuildingValidator_ValidNumberOfUnits_ReturnsValid(int numberOfUnits)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: numberOfUnits,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]     // Zero should be invalid
    [InlineData(-1)]    // Negative value
    [InlineData(10001)] // Above maximum allowed
    public void BuildingValidator_InvalidNumberOfUnits_ReturnsInvalid(int numberOfUnits)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: numberOfUnits,
            BuildingType: BuildingType.Residential,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(10)]    // Residential
    [InlineData(20)]    // Commercial
    [InlineData(30)]    // Industrial
    [InlineData(40)]    // MixedUse
    public void BuildingValidator_ValidBuildingType_ReturnsValid(int buildingTypeNum)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: 1,
            BuildingType: (BuildingType)buildingTypeNum,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]     // None (0) should be invalid
    [InlineData(1)]     // Not a defined enum value
    [InlineData(-1)]    // Negative value
    [InlineData(999)]   // Out of range
    public void BuildingValidator_InvalidBuildingType_ReturnsInvalid(int buildingTypeNum)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: 1,
            BuildingType: (BuildingType)buildingTypeNum,
            BuildingStatus: BuildingStatus.Active
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(10)]    // Active
    [InlineData(20)]    // UnderConstruction
    public void BuildingValidator_ValidBuildingStatus_ReturnsValid(int buildingStatusNum)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: 1,
            BuildingType: BuildingType.Residential,
            BuildingStatus: (BuildingStatus)buildingStatusNum
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]     // None (0) should be invalid
    [InlineData(1)]     // Not a defined enum value
    [InlineData(-1)]    // Negative value
    [InlineData(999)]   // Out of range
    public void BuildingValidator_InvalidBuildingStatus_ReturnsInvalid(int buildingStatusNum)
    {
        // Arrange
        var buildingDto = new SaveBuildingDTO
        (
            BuildingName: "Building A",
            BuildingAddress: "123 Main St",
            NumberOfUnits: 1,
            BuildingType: BuildingType.Residential,
            BuildingStatus: (BuildingStatus)buildingStatusNum
        );

        // Act
        var validator = new BuildingValidator();
        var result = validator.Validate(buildingDto);

        // Assert
        Assert.False(result.IsValid);
    }
}