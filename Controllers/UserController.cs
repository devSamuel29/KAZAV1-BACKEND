using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> register(RegisterRequest request)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<JsonModel>(
                await _userRepository.register(request)
            );

            if (response.code == 403)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    // public async Task<IActionResult> authenticate([FromBody] LoginRequest request)
    // {
    //     try
    //     {
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e);
    //     }
    // }
}
