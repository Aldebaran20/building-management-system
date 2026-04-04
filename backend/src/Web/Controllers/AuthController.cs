using Microsoft.AspNetCore.Mvc;
using BMS.Application.Interfaces;
using BMS.Application.DTOs.Auth;

namespace BMS.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/Auth/login
    [HttpPost("login")]
    [EndpointSummary("Login a user and receive a JWT token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<object>> Login(LoginDTO loginDto)
    {
        var tokenString = await _authService.LoginAsync(loginDto);        

        // If user is not found or password does not match, return unauthorized
        if (tokenString == null)
        {
            return Unauthorized();
        }

        return Ok(new { token = tokenString });
    }
}
