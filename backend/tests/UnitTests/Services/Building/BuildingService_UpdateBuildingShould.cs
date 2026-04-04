using NSubstitute;
using BMS.Application.Services;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;

namespace BMS.UnitTests.Services;

public class BuildingService_UpdateBuildingShould
{
    [Fact]
    public async Task UpdateBuilding_ExistingBuilding_ReturnsTrue()
    {
        // Arrange
        var existing = new Building
        {
            Id = 10,
            UserId = 1,
            BuildingName = "Building A",
            BuildingAddress = "123 Main St",
            NumberOfUnits = 1,
            BuildingType = BuildingType.Residential,
            BuildingStatus = BuildingStatus.Active,
            DateAdded = new DateOnly(2024, 1, 1)
        };

        var inputDto = new SaveBuildingDTO
        (
            "Building B",
            "245 Main St",
            20,
            BuildingType.Commercial,
            BuildingStatus.UnderConstruction
        );

        var expected = new Building
        {
            Id = 10,
            UserId = 1,
            BuildingName = "Building B",
            BuildingAddress = "245 Main St",
            NumberOfUnits = 20,
            BuildingType = BuildingType.Commercial,
            BuildingStatus = BuildingStatus.UnderConstruction,
            DateAdded = new DateOnly(2024, 1, 1)
        };

        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns(existing);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.UpdateBuildingAsync(10, inputDto, 1);

        // Assert
        Assert.True(result);

        // Equals method does not exist on entity class, so we check each property
        Assert.Equal(expected.Id, existing.Id);
        Assert.Equal(expected.BuildingName, existing.BuildingName);
        Assert.Equal(expected.BuildingAddress, existing.BuildingAddress);
        Assert.Equal(expected.NumberOfUnits, existing.NumberOfUnits);
        Assert.Equal(expected.BuildingType, existing.BuildingType);
        Assert.Equal(expected.BuildingStatus, existing.BuildingStatus);
        Assert.Equal(expected.DateAdded, existing.DateAdded);

        // Service UpdateBuildingAsync updates the existing building to be equal to
        //  the expected building, which it sends after mutation as a parameter to
        //  the repository method
        await mockRepository.Received(1)
            .UpdateBuildingAsync(existing);
    }

    [Fact]
    public async Task UpdateBuilding_NonExistent_ReturnsFalse()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO
        (
            "Building B",
            "245 Main St",
            20,
            BuildingType.Industrial,
            BuildingStatus.Active
        );

        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns((Building?)null);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.UpdateBuildingAsync(10, inputDto, 1);

        // Assert
        Assert.False(result);

        await mockRepository.DidNotReceive()
            .UpdateBuildingAsync(Arg.Any<Building>());
    }
}