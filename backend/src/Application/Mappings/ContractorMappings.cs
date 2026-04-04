using BMS.Domain.Entities;
using BMS.Application.DTOs.Contractors;

namespace BMS.Application.Mappings;

public static class ContractorMappingsExtensions
{
    public static ContractorDTO MapToContractorDto(this Contractor contractor)
    {
        return new ContractorDTO(
            contractor.Id,
            contractor.BusinessName,
            contractor.ContactName,
            contractor.ContactEmail,
            contractor.ContactPhone,
            contractor.AreaOfOperations,
            contractor.ContractorType,
            contractor.ContractorStatus,
            contractor.DateAdded
        );
    }

    public static Contractor MapToContractorEntity(this SaveContractorDTO dto, long userId)
    {
        return new Contractor
        {
            UserId = userId,
            BusinessName = dto.BusinessName,
            ContactName = dto.ContactName,
            ContactEmail = dto.ContactEmail,
            ContactPhone = dto.ContactPhone,
            AreaOfOperations = dto.AreaOfOperations,
            ContractorType = dto.ContractorType!.Value,
            ContractorStatus = dto.ContractorStatus!.Value,
            DateAdded = DateOnly.FromDateTime(DateTime.UtcNow)
        };
    }
}