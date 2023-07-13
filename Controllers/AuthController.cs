using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Auth;

using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthRepository authRepository, ILogger<AuthController> logger)
    {
        _authRepository = authRepository;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            return Ok(
                new JwtSecurityTokenHandler().WriteToken(
                    await _authRepository.LoginAsync(request)
                )
            );
        }
        catch (InvalidDataException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException)
        {
            return NotFound("Email ou senha incorretos!");
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "ERRO NO SERVIDOR, CONTACTAR DEV");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            return Ok(
                new JwtSecurityTokenHandler().WriteToken(
                    await _authRepository.RegisterAsync(request)
                )
            );
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (DbUpdateException e)
        {
            return Unauthorized(e.InnerException!.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "ERRO NO SERVIDOR, CONTACTAR DEV");
        }
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> CreateChangePassword(
        [FromBody] CreateChangePwdRequest request
    )
    {
        try
        {
            await _authRepository.CreateChangePasswordAsync(request);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound("Nenhum conta vinculada ao email fornecido!");
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "ERRO NO SERVIDOR, CONTACTAR DEV");
        }
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> UpdatePassword(
        [FromBody] ChangePasswordRequest request
    )
    {
        try
        {
            return Ok(
                new JwtSecurityTokenHandler().WriteToken(
                    await _authRepository.UpdatePasswordAsync(request)
                )
            );
        }
        catch (InvalidOperationException)
        {
            return NotFound("Nenhuma conta vinculada ao email fornecido!");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500, "ERRO NO SERVIDOR, CONTACTAR DEV");
        }
    }
}
