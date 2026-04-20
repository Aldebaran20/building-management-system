using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers.Buildings;

public class BuildingsController_DeleteBuildingShould
{
    [Fact]
    public async Task DeleteBuilding_ExistingBuilding_ReturnsNoContent()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        mockService.DeleteBuildingAsync(10, 1)
            .Returns(true);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);

        // Act
        var result = await buildingsController.DeleteBuilding(10);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await mockService.Received(1)
            .DeleteBuildingAsync(10, 1);
    }

    [Fact]
    public async Task DeleteBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        mockService.DeleteBuildingAsync(10, 1)
            .Returns(false);
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator, userId: 1);

        // Act
        var result = await buildingsController.DeleteBuilding(10);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        await mockService.Received(1)
            .DeleteBuildingAsync(10, 1);
    }

    [Fact]
    public async Task DeleteBuilding_InvalidUserId_ReturnsUnauthorized()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        var validator = new BuildingValidator();
        var buildingsController = BuildingsControllerFactory.Create(mockService, validator);

        // Act
        var result = await buildingsController.DeleteBuilding(10);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);

        await mockService.DidNotReceive()
            .DeleteBuildingAsync(Arg.Any<long>(), Arg.Any<long>());
    }
}