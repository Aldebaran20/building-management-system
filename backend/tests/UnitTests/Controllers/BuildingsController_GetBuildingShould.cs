using NSubstitute;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Buildings;

namespace BMS.UnitTests.Controllers;

public class BuildingsController_GetBuildingShould
{
    [Fact]
    public async Task GetBuilding_Existing_ReturnsOkResult()
    {
        // Arrange
        var expectedDto = new BuildingDTO
        (
            10,
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active,
            new DateOnly(2024, 1, 1)
        );

        var mockService = Substitute.For<IBuildingService>();
        mockService.GetBuildingByIdAsync(10, 1).Returns(expectedDto);
        var mockValidator = Substitute.For<IValidator<SaveBuildingDTO>>();
        var buildingsController = BuildingsControllerFactory.Create(mockService, mockValidator, userId: 1);

        // Act
        var result = await buildingsController.GetBuilding(10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDto = Assert.IsType<BuildingDTO>(okResult.Value, exactMatch: false);
        Assert.Same(expectedDto, returnedDto);

        await mockService.Received(1).GetBuildingByIdAsync(10, 1);
    }

    [Fact]
    public async Task GetBuilding_NonExisting_ReturnsNotFound()
    {
        // Arrange
        var mockService = Substitute.For<IBuildingService>();
        mockService.GetBuildingByIdAsync(10, 1).Returns((BuildingDTO?)null);
        var mockValidator = Substitute.For<IValidator<SaveBuildingDTO>>();
        var buildingsController = BuildingsControllerFactory.Create(mockService, mockValidator, userId: 1);

        // Act
        var result = await buildingsController.GetBuilding(10);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);

        await mockService.Received(1).GetBuildingByIdAsync(10, 1);
    }

    [Fact]
    public async Task GetBuilding_InvalidUserId_ReturnsUnauthorized()
    {
        // Arrange
        var expectedDto = new BuildingDTO
        (
            10,
            "Building A",
            "123 Main St",
            10,
            BuildingType.Residential,
            BuildingStatus.Active,
            new DateOnly(2024, 1, 1)
        );

        var mockService = Substitute.For<IBuildingService>();
        var mockValidator = Substitute.For<IValidator<SaveBuildingDTO>>();
        var buildingsController = BuildingsControllerFactory.Create(mockService, mockValidator);

        // Act
        var result = await buildingsController.GetBuilding(10);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);

        await mockService.DidNotReceive().GetBuildingByIdAsync(Arg.Any<long>(), Arg.Any<long>());
    }
}