using BMS.Application.DTOs.Contractors;
using BMS.Application.Mappings;
using BMS.Application.Interfaces;

namespace BMS.Application.Services;

public class ContractorService : IContractorService
{

    private readonly IContractorRepository _repository;

    public ContractorService(IContractorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ContractorDTO>> GetAllContractorsAsync(long userId)
    {
        var contractors = await _repository.GetAllContractorsAsync(userId);
        return contractors.Select(b => b.MapToContractorDto());
    }

    public async Task<ContractorDTO?> GetContractorByIdAsync(long contractorId, long userId)
    {     
        var contractor = await _repository.GetContractorByIdAsync(contractorId, userId);
        return contractor?.MapToContractorDto();
    }

    public async Task<ContractorDTO> CreateContractorAsync(SaveContractorDTO contractorDto, long userId)
    {
        var newContractor = contractorDto.MapToContractorEntity(userId);

        var addedContractor = await _repository.CreateContractorAsync(newContractor);

        return addedContractor.MapToContractorDto();
    }

    public async Task<bool> UpdateContractorAsync(long contractorId, SaveContractorDTO contractorDto, long userId)
    {
        var existingContractor = await _repository.GetContractorByIdAsync(contractorId, userId);
        if (existingContractor == null)
        {
            return false;
        }

        existingContractor.BusinessName = contractorDto.BusinessName;
        existingContractor.ContactName = contractorDto.ContactName;
        existingContractor.ContactEmail = contractorDto.ContactEmail;
        existingContractor.ContactPhone = contractorDto.ContactPhone;
        existingContractor.AreaOfOperations = contractorDto.AreaOfOperations;
        existingContractor.ContractorType = contractorDto.ContractorType!.Value;
        existingContractor.ContractorStatus = contractorDto.ContractorStatus!.Value;

        await _repository.UpdateContractorAsync(existingContractor);
        return true;
    }

    public async Task<bool> DeleteContractorAsync(long contractorId, long userId)
    {
        var existingContractor = await _repository.GetContractorByIdAsync(contractorId, userId);
        if (existingContractor == null)
        {
            return false;
        }

        await _repository.DeleteContractorAsync(existingContractor);
        return true;
    }

}
