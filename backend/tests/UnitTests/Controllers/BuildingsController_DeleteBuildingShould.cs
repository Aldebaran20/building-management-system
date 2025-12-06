using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Web.Controllers;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers;

public class BuildingsController_DeleteBuildingShould
{
    [Fact]
    public async Task DeleteBuilding_ExistingBuilding_ReturnsNoContent()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        mockService.DeleteBuildingAsync(10)
            .Returns(true);
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);

        // Act
        var result = await buildingsController.DeleteBuilding(10);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await mockService.Received(1)
            .DeleteBuildingAsync(10);
    }

    [Fact]
    public async Task DeleteBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        mockService.DeleteBuildingAsync(10)
            .Returns(false);
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);

        // Act
        var result = await buildingsController.DeleteBuilding(10);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        await mockService.Received(1)
            .DeleteBuildingAsync(10);
    }
}