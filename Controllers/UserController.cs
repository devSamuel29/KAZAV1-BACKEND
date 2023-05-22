using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("/get-users/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync(int skip, int take)
    {
        try
        {
            return Ok(await _userRepository.GetAllUsersAsync(skip, take));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            return Ok(await _userRepository.GetUserByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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

    [HttpPatch("update-user-password/{id}")]
    public async Task<IActionResult> UpdatePasswordUser(int id, ForgottenPasswordRequest request)
    {
        try
        {
            return Ok(await _userRepository.UpdatePasswordUser(id, request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("remove-users/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(int skip, int take)
    {
        try
        {
            return Ok(await _userRepository.DeleteAllUsersAsync(skip, take));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("remove-user/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        try
        {
            return Ok(await _userRepository.DeleteUserByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
