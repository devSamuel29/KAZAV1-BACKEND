using kazariobranco_backend.Repository.IRepository;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return (IActionResult) await _contactRepository.createContactOrder(request);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
