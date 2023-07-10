using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Auth;

using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authRepository.LoginAsync(request);
            return Ok(new JwtSecurityTokenHandler().WriteToken(response));
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            await _authRepository.RegisterAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPost("forgot-my-password")]
    public async Task<IActionResult> CreateChangePassword([FromHeader] string email)
    {
        try
        {
            await _authRepository.CreateChangePasswordAsync(email);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpPut("forgot-my-password")]
    public async Task<IActionResult> UpdatePassword(
        [FromBody] ForgottenPasswordRequest request
    )
    {
        try
        {
            await _authRepository.UpdatePasswordAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}
