using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Contact;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactController;

    private readonly IAdminRepository _adminRepository;

    public ContactController(
        IContactRepository contactRepository,
        IAdminRepository adminRepository
    )
    {
        _contactController = contactRepository;
        _adminRepository = adminRepository;
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
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContact = await _adminRepository.ReadContactByIdAsync(
                headerValue!,
                id
            );
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
    [HttpGet("read-contacts-by-name/{name}")]
    public async Task<IActionResult> ReadContactsByNameAsync([FromRoute] string name)
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.ReadContactsByNameAsync(
                headerValue!,
                name
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
    [HttpGet("read-contacts-by-email/{email}")]
    public async Task<IActionResult> ReadContactsByEmailAsync([FromRoute] string email)
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.ReadContactsByEmailAsync(
                headerValue!,
                email
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
    [HttpGet("read-contacts-by-phone/{phone}")]
    public async Task<IActionResult> ReadContactsByPhoneAsync([FromRoute] string phone)
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.ReadContactsByPhoneAsync(
                headerValue!,
                phone
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
    [HttpGet("read-all-contacts/{skip}/{take}/{orderByDate?}")]
    public async Task<IActionResult> ReadContactsInRangeAsync(
        [FromRoute] int skip,
        [FromRoute] int take,
        [FromRoute] bool? orderByDate
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.ReadContactsInRangeAsync(
                headerValue!,
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
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.UpdateStatusByIdAsync(
                headerValue!,
                id
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
    [HttpPatch("update-status-in-range/{skip}/{take}")]
    public async Task<IActionResult> UpdateStatusInRangeAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbContacts = await _adminRepository.UpdateStatusInRangeAsync(
                headerValue!,
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
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _adminRepository.DeleteContactByIdAsync(headerValue!, id);
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
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _adminRepository.DeleteContactsInRangeAsync(headerValue!, skip, take);
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
