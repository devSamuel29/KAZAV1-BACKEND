using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.AdminUserPolicyName)]
[ApiController]
[Route("v1/api/[controller]")]
public class AdminToContactController : ControllerBase
{
    private readonly IAdminToContactRepository _adminToContactRepository;

    public AdminToContactController(IAdminToContactRepository adminToContactRepository)
    {
        _adminToContactRepository = adminToContactRepository;
    }

    [HttpGet("get-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> GetAllContactsAsync(int skip, int take)
    {
        try
        {
            var _dbContacts = await _adminToContactRepository.GetAllContactsAsync(skip, take);
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
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var _dbContact = await _adminToContactRepository.GetContactByIdAsync(id);
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
    public async Task<IActionResult> UpdateAllContactsAsync(int skip, int take)
    {
        try
        {
            var _dbContacts = await _adminToContactRepository.UpdateAllStatusAsync(skip, take);
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
    public async Task<IActionResult> UpdateStatusByIdAsync(int id)
    {
        try
        {
            var _dbContacts = await _adminToContactRepository.UpdateStatusByIdAsync(id);
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
    public async Task<IActionResult> DeleteAllContactsAsync(int skip, int take)
    {
        try
        {
            var _dbContacts = await _adminToContactRepository.DeleteAllContactsAsync(skip, take);
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
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
            var _dbContact = await _adminToContactRepository.DeleteContactByIdAsync(id);
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
