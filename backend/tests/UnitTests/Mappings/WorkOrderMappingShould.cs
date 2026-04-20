using BMS.Domain.Entities;
using BMS.Application.DTOs.WorkOrders;
using BMS.Application.Mappings;

namespace BMS.UnitTests.Mappings;

public class WorkOrderMappingsShould
{
    [Fact]
    public void MapToWorkOrderDto_ReturnsCorrectDto()
    {
        // Arrange
        var inputEntity = new WorkOrder
        {
            Id = 10,
            UserId = 1,
            BuildingId = 20,
            Building = new Building
            {
                Id = 20,
                UserId = 1,
                BuildingName = "Building A",
                BuildingAddress = "123 Main St",
                BuildingType = BuildingType.Residential
            },
            ContractorId = 30,
            Contractor = new Contractor
            {
                Id = 30,
                BusinessName = "Contractor Inc.",
                ContactName = "John Doe",
                ContactEmail = "john.doe@gmail.com",
                ContactPhone = "1203129123",
                ContractorType = ContractorType.Plumbing
            },
            Title = "Fix plumbing",
            Description = "The plumbing in the building is leaking.",
            Status = WorkOrderStatus.Pending,
            Priority = WorkOrderPriority.High,
            DateCreated = new DateOnly(2027, 1, 1),
            Deadline = new DateOnly(2027, 1, 15),
        };

        var workOrderBuilding = new WorkOrderBuildingDTO(
            20,
            "Building A",
            "123 Main St",
            BuildingType.Residential
        );

        var workOrderContractor = new WorkOrderContractorDTO(
            30,
            BusinessName: "Contractor Inc.",
            ContactName: "John Doe",
            ContactEmail: "john.doe@gmail.com",
            ContactPhone: "1203129123",
            ContractorType: ContractorType.Plumbing
        );

        var expectedDto = new WorkOrderDTO(
            40,
            Building: workOrderBuilding,
            Contractor: workOrderContractor,
            Title: "Fix plumbing",
            Description: "The plumbing in the building is leaking.",
            Status: WorkOrderStatus.Pending,
            Priority: WorkOrderPriority.High,
            DateCreated: new DateOnly(2027, 1, 1),
            Deadline: new DateOnly(2027, 1, 15),
            null
        );

        // Act
        var result = inputEntity.MapToWorkOrderDto();

        // Assert
        Assert.Equal(expectedDto, result);
    }

    // [Fact]
    // public void MapToBuildingEntity_ReturnsCorrectEntity()
    // {
    //     // Arrange
    //     var inputDto = new SaveBuildingDTO(
    //         "Building A",
    //         "123 Main St",
    //         10,
    //         BuildingType.Residential,
    //         BuildingStatus.Active
    //     );

    //     // Act
    //     var result = inputDto.MapToBuildingEntity(1);

    //     // Assert
    //     Assert.Equal(1, result.UserId);
    //     Assert.Equal("Building A", result.BuildingName);
    //     Assert.Equal("123 Main St", result.BuildingAddress);
    //     Assert.Equal(10, result.NumberOfUnits);
    //     Assert.Equal(BuildingType.Residential, result.BuildingType);
    //     Assert.Equal(BuildingStatus.Active, result.BuildingStatus);
    //     Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow), result.DateAdded);
    // }
}