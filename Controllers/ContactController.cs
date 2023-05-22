using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace kazariobranco_backend.Controllers;
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpPost]
    [Route("create-contact-order")]
    public async Task<IActionResult> createContactOrder([FromBody] ContactRequest request)
    {
        try
        {
            var response = await _contactRepository.CreateContactAsync(request);
            return Ok(response.Message);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [Authorize]
    // [HttpGet]
    // public async Task<IActionResult> getAllContacts()
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.ToString());
    //     }
    // }

    // [Authorize]
    // [HttpGet]
    // public async Task<IActionResult> getContactById(int id)
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.ToString());
    //     }
    // }

    // [Authorize]
    // [HttpPatch]
    // public async Task<IActionResult> updateStatusById(int id)
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.ToString());
    //     }
    // }

    // [Authorize]
    // [HttpDelete]
    // public async Task<IActionResult> deleteAllContacts()
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.ToString());
    //     }
    // }

    // [Authorize]
    // [HttpDelete]
    // public async Task<IActionResult> deleteContactById()
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.ToString());
    //     }
    // }
}
