using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.Data.SqlClient;
using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost("register-user")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var response = await _userRepository.Register(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("authenticate-user")]
    public async Task<IActionResult> Authenticate(LoginRequest request)
    {
        try
        {
            var response = await _userRepository.Authenticate(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPatch("update-user-password/{id}")]
    public async Task<IActionResult> UpdatePasswordUser(int id, ForgottenPasswordRequest request)
    {
        try
        {
            return Ok(await _userRepository.UpdatePasswordUser(id, request));
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (SqlException e)
        {
            return BadRequest(e.Message);
        }
    }
}
