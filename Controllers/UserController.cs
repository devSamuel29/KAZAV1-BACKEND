using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.Data.SqlClient;

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

    [HttpGet("get-users/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync(int skip, int take)
    {
        try
        {
            return Ok(await _userRepository.GetAllUsersAsync(skip, take));
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

    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            return Ok(await _userRepository.GetUserByIdAsync(id));
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

    // *********
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

    // *********
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
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (SqlException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-users/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(int skip, int take)
    {
        try
        {
            return Ok(await _userRepository.DeleteAllUsersAsync(skip, take));
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

    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        try
        {
            return Ok(await _userRepository.DeleteUserByIdAsync(id));
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
