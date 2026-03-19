using BMS.Domain.Entities;

namespace BMS.Application.Interfaces;

public interface IAuthRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
}