using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Request;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;
using System.Net.Http.Headers;
using Microsoft.Extensions.Primitives;

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

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("get-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbUsers = await _userRepository.GetAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("get-user-by-id-async/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbUser = await _userRepository.GetUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("my-data")]
    public async Task<IActionResult> MyDataAsync([FromBody] JwtRequest request)
    {
        try
        {
            return Ok(await _userRepository.MyDataAsync(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("register-address")]
    public async Task<IActionResult> RegisterAddress(
        [FromBody] AddNewAddressRequest request
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _userRepository.RegisterAddressAsync(headerValue, request);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbUsers = await _userRepository.DeleteAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-user-by-id-async/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbUser = await _userRepository.DeleteUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
