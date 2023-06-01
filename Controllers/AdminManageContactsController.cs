using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.AdminUserPolicyName)]
[ApiController]
[Route("v1/api/[controller]")]
public class AdminManageContactsController : ControllerBase
{
    private readonly IAdminManageContactsRepository _adminManageContactsRepository;

    public AdminManageContactsController(
        IAdminManageContactsRepository adminManageContactsRepository
    )
    {
        _adminManageContactsRepository = adminManageContactsRepository;
    }

    [HttpGet("get-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> GetAllContactsAsync([FromRoute] int skip, [FromRoute] int take)
    {
        try
        {
            var _dbContacts = await _adminManageContactsRepository.GetAllContactsAsync(skip, take);
            return Ok(_dbContacts);
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

    [HttpGet("get-contact-by-id/{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        try
        {
            var _dbContact = await _adminManageContactsRepository.GetContactByIdAsync(id);
            return Ok(_dbContact);
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

    [HttpPatch("update-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> UpdateAllContactsAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbContacts = await _adminManageContactsRepository.UpdateAllStatusAsync(skip, take);
            return Ok(_dbContacts);
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

    [HttpPatch("update-contact-by-id/{id}")]
    public async Task<IActionResult> UpdateStatusByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbContacts = await _adminManageContactsRepository.UpdateStatusByIdAsync(id);
            return Ok(_dbContacts);
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

    [HttpDelete("delete-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllContactsAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbContacts = await _adminManageContactsRepository.DeleteAllContactsAsync(
                skip,
                take
            );
            return Ok(_dbContacts);
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

    [HttpDelete("delete-contact-by-id/{id}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] int id)
    {
        try
        {
            var _dbContact = await _adminManageContactsRepository.DeleteContactByIdAsync(id);
            return Ok(_dbContact);
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
