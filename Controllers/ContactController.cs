using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kazariobranco_backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> createContactOrder([FromBody] ContactRequest request)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<JsonModel>(
                    await _contactRepository.createContactOrder(request)
                );

            if (response.code == 403)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
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
