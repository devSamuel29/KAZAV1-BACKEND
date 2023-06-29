using System.IdentityModel.Tokens.Jwt;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

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
            return Created(
                "minha url",
                new
                {
                    name = $"{request.Firstname} {request.Lastname}",
                    Email = request.Email,
                }
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }
}
