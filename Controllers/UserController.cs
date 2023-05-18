using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> register(RegisterRequest request)
    {
        try
        {
            var response = await _userRepository.register(request);

            switch (response.code)
            {
                case 400:
                    return BadRequest(response.message);
                case 406:
                    return BadRequest(response.message);
                default:
                    return Ok(response.message);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("/authenticate")]
    public async Task<IActionResult> authenticate([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _userRepository.authenticate(request);

            switch (response.code)
            {
                case 400:
                    return BadRequest(response.message);
                case 401:
                    return BadRequest(response.message);
                case 406:
                    return BadRequest(response.message);
                default:
                    return Ok(response.message);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
