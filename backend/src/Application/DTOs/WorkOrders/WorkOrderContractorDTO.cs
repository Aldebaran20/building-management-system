using BMS.Domain.Entities;

namespace BMS.Application.DTOs.WorkOrders;

public record WorkOrderContractorDTO(
    long Id,
    string? BusinessName,
    string? ContactName,
    string? ContactEmail,
    string ContactPhone,
    ContractorType ContractorType
);