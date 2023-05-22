using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpGet("get-contacts/{skip}/{take}")]
    public async Task<IActionResult> GetAllContactsAsync(int skip, int take)
    {
        try
        {
            return Ok(await _contactRepository.GetAllContactsAsync(skip, take));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get-contact/{id}")]
    public async Task<IActionResult> GetContactByIdAsync(int id)
    {
        try
        {
            return Ok(await _contactRepository.GetContactByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpGet("/get-contact/name/{name}")]
    // public async Task<IActionResult> GetContactsByNameAsync(string name)
    // {
    //     try
    //     {
    //         return Ok(await _contactRepository.GetContactByNameAsync(name));
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // [HttpGet("/get-contact/names/{name}")]
    // public async Task<IActionResult> GetContactByNameAsync(string name)
    // {
    //     try
    //     {
    //         return Ok(await _contactRepository.GetContactsByNameAsync(name));
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // [HttpGet("/get-contact/email/{email}")]
    // public async Task<IActionResult> GetContactByEmailAsync(string email)
    // {
    //     try
    //     {
    //         return Ok(await _contactRepository.GetContactByEmailAsync(email));
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    [HttpPost("create-contact")]
    public async Task<IActionResult> CreateContactOrder(ContactRequest request)
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

    [HttpPatch("update-status/{id}")]
    public async Task<IActionResult> UpdateStatusContactByIdAsync(int id)
    {
        try
        {
            return Ok(await _contactRepository.UpdateStatusContactByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-contacts/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllContactsAsync(int skip, int take)
    {
        try
        {
            return Ok(await _contactRepository.DeleteAllContactsAsync(skip, take));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-contact/{id}")]
    public async Task<IActionResult> DeleteContactByIdAsync(int id)
    {
        try
        {
            return Ok(await _contactRepository.DeleteContactById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
