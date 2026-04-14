using BMS.Domain.Entities;

namespace BMS.Application.DTOs.WorkOrders;

public record WorkOrderDTO(
    long Id,
    WorkOrderBuildingDTO Building,
    WorkOrderContractorDTO Contractor,
    string Title,
    string? Description,
    WorkOrderStatus Status,
    WorkOrderPriority? Priority,
    DateOnly DateCreated,
    DateOnly? Deadline,
    DateOnly? DateCompleted
);