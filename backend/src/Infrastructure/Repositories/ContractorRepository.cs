using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;


namespace BMS.Infrastructure.Repositories;

public class ContractorRepository : IContractorRepository
{

    private readonly ApplicationDbContext _context;

    public ContractorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contractor>> GetAllContractorsAsync(long userId)
    {
        return await _context.Contractors
            .Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<Contractor?> GetContractorByIdAsync(long contractorId, long userId)
    {
        return await _context.Contractors
            .FirstOrDefaultAsync(c => c.Id == contractorId && c.UserId == userId);
    }

    public async Task<Contractor> CreateContractorAsync(Contractor contractor)
    {
        _context.Contractors.Add(contractor);
        await _context.SaveChangesAsync();
        return contractor;
    }

    public async Task UpdateContractorAsync(Contractor contractor)
    {
        _context.Entry(contractor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContractorAsync(Contractor contractor)
    {
        _context.Contractors.Remove(contractor);
        await _context.SaveChangesAsync();
    }
}