using BMS.Application.DTOs.Auth;
using BMS.Application.Interfaces;
using BMS.Application.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace BMS.Application.Services;

public class AuthService : IAuthService
{

    private readonly IAuthRepository _repository;
    private readonly JwtOptions _jwtOptions;

    public AuthService(IAuthRepository repository, IOptions<JwtOptions> jwtOptions)
    {
        _repository = repository;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string?> LoginAsync(LoginDTO loginDto)
    {
        var user = await _repository.GetUserByEmailAsync(loginDto.Email.ToLowerInvariant());

        // If user is not found or password does not match, return null
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.HashedPassword))
        {
            return null;
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(new SecurityTokenDescriptor
        {
            // Holds what the user claims to be, in this case, their email
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            // Token expires in 1 hour
            Expires = DateTime.UtcNow.AddHours(1),
            // Sign the token with a secret key
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.Key!)
                ),
                SecurityAlgorithms.HmacSha256Signature
            )
        });

        return handler.WriteToken(token);
    }
}
