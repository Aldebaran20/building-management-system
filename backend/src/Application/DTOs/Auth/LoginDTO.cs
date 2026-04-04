namespace BMS.Application.DTOs.Auth;

public record LoginDTO(
    string Email,
    string Password
);