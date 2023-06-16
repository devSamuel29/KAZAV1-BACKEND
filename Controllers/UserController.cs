using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request;
using Microsoft.Data.SqlClient;
using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Controllers;

[Authorize(Policy = IdentityData.UserPolicyName)]
[ApiController]
[Route("v1/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("get-my-data")]
    public async Task<IActionResult> GetMyDataAsync([FromBody] JwtRequest request)
    {
        try
        {
            return Ok(await _userRepository.GetMyDataAsync(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update-user-password/{id}")]
    public async Task<IActionResult> UpdatePasswordUser(
        [FromBody] ForgottenPasswordRequest request,
        [FromRoute] int id
    )
    {
        try
        {
            await _userRepository.UpdatePasswordUserAsync(request, id);
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

    [HttpPost("register-user-address/{id}/{email}")]
    public async Task<IActionResult> RegisterAddress(
        [FromBody] AddressRequest request,
        [FromRoute] int id,
        [FromRoute] string email
    )
    {
        try
        {
            // await _userRepository.RegisterAddressAsync(request, id, email);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
