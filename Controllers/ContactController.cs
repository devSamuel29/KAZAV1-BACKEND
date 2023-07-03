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

    [HttpPost("create-contact")]
    public async Task<IActionResult> CreateContactAsync([FromBody] ContactRequest request)
    {
        try
        {
            await _contactController.CreateContactAsync(request);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("read-contact-by-id/{id}")]
    public async Task<IActionResult> ReadContactByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbContact = await _contactController.ReadContactByIdAsync(id);
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

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("read-contacts-by-email/{email}")]
    public async Task<IActionResult> ReadContactsByEmailAsync([FromRoute] string email)
    {
        try
        {
            var _dbContacts = await _contactController.ReadContactsByEmailAsync(email);
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
    [HttpGet("read-contacts-by-phone/{phone}")]
    public async Task<IActionResult> ReadContactsByNameAsync([FromRoute] string phone)
    {
        try
        {
            var _dbContacts = await _contactController.ReadContactsByPhoneAsync(phone);
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
    [HttpGet("read-all-contacts/{skip}/{take}/{orderByDate?}")]
    public async Task<IActionResult> ReadContactsInRangeAsync(
        [FromRoute] int skip,
        [FromRoute] int take,
        [FromRoute] bool? orderByDate
    )
    {
        try
        {
            var _dbContacts = await _contactController.ReadContactsInRangeAsync(
                skip,
                take,
                orderByDate
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
    [HttpPatch("update-status-in-range/{skip}/{take}")]
    public async Task<IActionResult> UpdateStatusInRangeAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbContacts = await _contactController.UpdateStatusInRangeAsync(
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
            await _contactController.DeleteContactByIdAsync(id);
            return NoContent();
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
            await _contactController.DeleteContactsInRangeAsync(skip, take);
            return NoContent();
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
