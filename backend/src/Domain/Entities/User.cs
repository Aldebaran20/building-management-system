namespace BMS.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }
}