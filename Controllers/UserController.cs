using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("/get-users")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            return Ok(await _userRepository.GetAllUsersAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("/get-user/{id}")]
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
    [HttpPost]
    [Route("/register-user")]
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
    [HttpPost]
    [Route("/authenticate-user")]
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

    [HttpPatch]
    [Route("/update-user-password/{id}")]
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

    [HttpDelete]
    [Route("/remove-users")]
    public async Task<IActionResult> DeleteAllUsersAsync()
    {
        try
        {
            return Ok(await _userRepository.DeleteAllUsersAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("/remove-user/{id}")]
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
