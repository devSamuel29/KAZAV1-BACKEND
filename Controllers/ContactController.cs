using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactController;

    public ContactController(IContactRepository contactRepository)
    {
        _contactController = contactRepository;
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("get-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> GetAllContactsAsync([FromRoute] int skip, [FromRoute] int take)
    {
        try
        {
            var _dbContacts = await _contactController.GetAllContactsAsync(skip, take);
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

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("get-contact-by-id/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbContact = await _contactController.GetContactByIdAsync(id);
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

    [HttpPost("create-contact")]
    public async Task<IActionResult> CreateContactAsync([FromBody] ContactRequest request)
    {
        try
        {
            await _contactController.CreateContactAsync(request);
            return NoContent();
        }
        catch(Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPatch("update-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> UpdateAllContactsAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbContacts = await _contactController.UpdateAllStatusAsync(skip, take);
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

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPatch("update-contact-by-id/{id}")]
    public async Task<IActionResult> UpdateStatusByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbContacts = await _contactController.UpdateStatusByIdAsync(id);
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

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-all-contacts/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllContactsAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbContacts = await _contactController.DeleteAllContactsAsync(
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
    
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-contact-by-id/{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbContact = await _contactController.DeleteContactByIdAsync(id);
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
