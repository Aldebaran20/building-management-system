using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs;
using BMS.Web.Controllers;
using BMS.Application.Validators;

namespace BMS.UnitTests.Controllers;

public class BuildingsController_PutBuildingShould
{
    [Fact]
    public async Task PutBuilding_ValidBuilding_ReturnsNoContent()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO
        (
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active
        );

        var mockService = Substitute.For<IBuildingService>();
        mockService.UpdateBuildingAsync(10, inputDto)
            .Returns(true);
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);

        // Act
        var result = await buildingsController.PutBuilding(10, inputDto);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await mockService.Received(1)
            .UpdateBuildingAsync(10, inputDto);
    }

    [Fact]
    public async Task PutBuilding_InvalidBuilding_ReturnsBadRequest()
    {
        // Arrange
        var invalidDto = new SaveBuildingDTO
        (
            "2",
            "shrt",
            0,
            BuildingType.None,
            BuildingStatus.None
        );

        var mockService = Substitute.For<IBuildingService>();
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);


        // Act
        var result = await buildingsController.PutBuilding(10, invalidDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);

        await mockService.Received(0)
            .UpdateBuildingAsync(10, invalidDto);
    }

    [Fact]
    public async Task PutBuilding_NonExistent_ReturnsNotFound()
    {
        // Arrange
        var inputDto = new SaveBuildingDTO
        (
            "Building A",
            "123 Main St",
            1,
            BuildingType.Residential,
            BuildingStatus.Active
        );

        var mockService = Substitute.For<IBuildingService>();
        mockService.UpdateBuildingAsync(10, inputDto)
            .Returns(false);
        var validator = new BuildingValidator();
        var buildingsController = new BuildingsController(mockService, validator);


        // Act
        var result = await buildingsController.PutBuilding(10, inputDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);

        await mockService.Received(1)
            .UpdateBuildingAsync(10, inputDto);
    }
}