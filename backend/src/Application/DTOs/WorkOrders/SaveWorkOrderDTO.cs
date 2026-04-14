using BMS.Domain.Entities;

namespace BMS.Application.DTOs.WorkOrders;

public record SaveWorkOrderDTO(
    long BuildingId,
    long ContractorId,
    string Title,
    string? Description,
    WorkOrderPriority? Priority,
    DateOnly? Deadline
);