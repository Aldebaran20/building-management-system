using BMS.Domain.Entities;

namespace BMS.Application.DTOs.WorkOrders;

public record WorkOrderBuildingDTO(
    long Id,
    string BuildingName,
    string BuildingAddress,
    BuildingType BuildingType
);