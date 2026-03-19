using Microsoft.EntityFrameworkCore;
using BMS.Domain.Entities;
using BMS.Application.Interfaces;

namespace BMS.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{

    private readonly ApplicationDbContext _context;

    public AuthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}