using BMS.Application.DTOs.Auth;

namespace BMS.Application.Interfaces;

public interface IAuthService
{
    public Task<string?> LoginAsync(LoginDTO loginDto);
}