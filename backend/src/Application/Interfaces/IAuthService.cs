using BMS.Application.DTOs;

namespace BMS.Application.Interfaces;

public interface IAuthService
{
    public Task<string?> LoginAsync(LoginDTO loginDto);
}