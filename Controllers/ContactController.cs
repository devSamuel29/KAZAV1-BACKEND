using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpPost("create-contact")]
    public async Task<IActionResult> CreateContact(ContactRequest request)
    {
        try
        {
            await _contactRepository.CreateContactAsync(request);
            return Ok();
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
        catch (SqlException e)
        {
            return BadRequest(e.Message);
        }
    }
}
