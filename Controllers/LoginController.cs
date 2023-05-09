using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Request;
using kazariobranco_backend.Interfaces;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> authenticate([FromBody] LoginRequest request)
    {
        try
        {
            return (IActionResult) await _userRepository.authenticate(request);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
