using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.AdminUserPolicyName)]
[ApiController]
[Route("v1/api/[controller]")]
public class AdminToUserController : ControllerBase
{
    private readonly IAdminToUserRepository _adminToUserRepository;

    public AdminToUserController(IAdminToUserRepository adminToUserRepository)
    {
        _adminToUserRepository = adminToUserRepository;
    }

    [HttpGet("get-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync(int skip, int take)
    {
        try
        {
            var _dbUsers = await _adminToUserRepository.GetAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpGet("get-user-by-id-async/{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            var _dbUser = await _adminToUserRepository.GetUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(int skip, int take)
    {
        try
        {
            var _dbUsers = await _adminToUserRepository.DeleteAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-user-by-id-async/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        try
        {
            var _dbUser = await _adminToUserRepository.DeleteUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
