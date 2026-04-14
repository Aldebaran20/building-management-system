using BMS.Domain.Entities;
using BMS.Application.DTOs.WorkOrders;

namespace BMS.Application.Mappings;

public static class WorkOrderMappingsExtensions
{
    public static WorkOrderDTO MapToWorkOrderDto(this WorkOrder workOrder)
    {
        var building = new WorkOrderBuildingDTO(
            workOrder.BuildingId,
            workOrder.Building.BuildingName,
            workOrder.Building.BuildingAddress,
            workOrder.Building.BuildingType
        );

        var contractor = new WorkOrderContractorDTO(
            workOrder.ContractorId,
            workOrder.Contractor.BusinessName,
            workOrder.Contractor.ContactName,
            workOrder.Contractor.ContactEmail,
            workOrder.Contractor.ContactPhone,
            workOrder.Contractor.ContractorType
        );

        return new WorkOrderDTO(
            workOrder.Id,
            building,
            contractor,
            workOrder.Title,
            workOrder.Description,
            workOrder.Status,
            workOrder.Priority,
            workOrder.DateCreated,
            workOrder.Deadline,
            workOrder.DateCompleted
        );
    }

    public static WorkOrder MapToWorkOrderEntity(this SaveWorkOrderDTO dto, long userId)
    {
        return new WorkOrder
        {
            UserId = userId,
            BuildingId = dto.BuildingId,
            ContractorId = dto.ContractorId,
            Title = dto.Title,
            Description = dto.Description,
            Status = WorkOrderStatus.Pending, // Default status for new work orders
            Priority = dto.Priority,
            DateCreated = DateOnly.FromDateTime(DateTime.UtcNow),
            Deadline = dto.Deadline
        };
    }
}