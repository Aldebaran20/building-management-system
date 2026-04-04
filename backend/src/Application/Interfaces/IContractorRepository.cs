using BMS.Domain.Entities;

namespace BMS.Application.Interfaces;

public interface IContractorRepository
{
    public Task<IEnumerable<Contractor>> GetAllContractorsAsync(long userId);
    public Task<Contractor?> GetContractorByIdAsync(long contractorId, long userId);
    public Task<Contractor> CreateContractorAsync(Contractor contractor);
    public Task UpdateContractorAsync(Contractor contractor);
    public Task DeleteContractorAsync(Contractor contractor);
}