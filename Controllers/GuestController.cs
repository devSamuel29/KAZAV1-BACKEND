using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Data.SqlClient;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IGuestRepository _guestRepository;

    public GuestController(IConfiguration config, IGuestRepository guestRepository)
    {
        _config = config;
        _guestRepository = guestRepository;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        try
        {
            await _guestRepository.RegisterAsync(request);
            return NoContent();
        }
        catch (SqlException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login-user")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _guestRepository.LoginAsync(request);
            var token = new JwtSecurityTokenHandler().WriteToken(response);

            return Ok(token);
        }
        catch (SqlException e)
        {
            return BadRequest(e.Message);
        }
    }
}
