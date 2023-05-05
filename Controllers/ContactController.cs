using kazariobranco_backend.Repository.IRepository;
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

    
}
