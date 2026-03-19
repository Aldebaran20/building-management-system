using BMS.Domain.Entities;

namespace BMS.Application.DTOs;

public record LoginDTO(
    string Email,
    string Password
);