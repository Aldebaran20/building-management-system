using BMS.Application.DTOs.Contractors;

namespace BMS.Application.Interfaces;

public interface IContractorService
{
    public Task<IEnumerable<ContractorDTO>> GetAllContractorsAsync(long userId);
    public Task<ContractorDTO?> GetContractorByIdAsync(long contractorId, long userId);
    public Task<ContractorDTO> CreateContractorAsync(SaveContractorDTO contractorDto, long userId);
    public Task<bool> UpdateContractorAsync(long contractorId, SaveContractorDTO contractorDto, long userId);
    public Task<bool> DeleteContractorAsync(long contractorId, long userId);
}