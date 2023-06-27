using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Request;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.UserPolicyName)]
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

    [HttpPost("get-my-data")]
    public async Task<IActionResult> GetMyDataAsync([FromBody] JwtRequest request)
    {
        try
        {
            return Ok(await _userRepository.GetMyDataAsync(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpPatch("update-user-password/{id}")]
    // public async Task<IActionResult> UpdatePasswordUser(
    //     [FromBody] ForgottenPasswordRequest request,
    //     [FromRoute] int id
    // )
    // {
    //     try
    //     {
    //         await _userRepository.UpdatePasswordUserAsync(request);
    //         return NoContent();
    //     }
    //     catch (NullReferenceException e)
    //     {
    //         return NotFound(e.Message);
    //     }
    //     catch (SqlException e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // [HttpPost("register-user-address")]
    // public async Task<IActionResult> RegisterAddressAsync(
    //     [FromBody] JwtRequest jwtRequest
    // )
    // {
    //     try
    //     {
    //         await _userRepository.RegisterAddressAsync(jwtRequest);
    //         return NoContent();
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
}
