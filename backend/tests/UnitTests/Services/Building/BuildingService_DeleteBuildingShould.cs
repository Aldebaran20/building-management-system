using NSubstitute;
using BMS.Application.Services;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;

namespace BMS.UnitTests.Services;

public class BuildingService_DeleteBuildingShould
{
    [Fact]
    public async Task DeleteBuilding_ExistingBuilding_ReturnsTrue()
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

        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns(existing);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.DeleteBuildingAsync(10, 1);

        // Assert
        Assert.True(result);

        await mockRepository.Received(1)
            .DeleteBuildingAsync(Arg.Is<Building>(b => b.Id == 10 && b.UserId == 1));
    }

    [Fact]
    public async Task DeleteBuilding_NonExistent_ReturnsFalse()
    {
        // Arrange
        var mockRepository = Substitute.For<IBuildingRepository>();
        mockRepository.GetBuildingByIdAsync(10, 1)
            .Returns((Building?)null);
        var buildingService = new BuildingService(mockRepository);

        // Act
        var result = await buildingService.DeleteBuildingAsync(10, 1);

        // Assert
        Assert.False(result);

        await mockRepository.DidNotReceive()
            .DeleteBuildingAsync(Arg.Any<Building>());
    }
}