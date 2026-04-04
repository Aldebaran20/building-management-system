using BMS.Domain.Entities;

namespace BMS.Application.DTOs.Contractors;

public record ContractorDTO(
    long Id,
    string? BusinessName,
    string? ContactName,
    string? ContactEmail,
    string ContactPhone,
    string? AreaOfOperations,
    ContractorType ContractorType,
    ContractorStatus ContractorStatus,
    DateOnly DateAdded
);