using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.AdminPolicyName)]
[ApiController]
[Route("v1/api/[controller]")]
public class AdminManageUsersController : ControllerBase
{
    private readonly IAdminManageUsersRepository _adminMangeUsersRepository;

    public AdminManageUsersController(IAdminManageUsersRepository adminMangeUsersRepository)
    {
        _adminMangeUsersRepository = adminMangeUsersRepository;
    }

    [HttpGet("get-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync([FromRoute] int skip, [FromRoute] int take)
    {
        try
        {
            var _dbUsers = await _adminMangeUsersRepository.GetAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [HttpGet("get-user-by-id-async/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbUser = await _adminMangeUsersRepository.GetUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync([FromRoute] int skip, [FromRoute] int take)
    {
        try
        {
            var _dbUsers = await _adminMangeUsersRepository.DeleteAllUsersAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-user-by-id-async/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbUser = await _adminMangeUsersRepository.DeleteUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
