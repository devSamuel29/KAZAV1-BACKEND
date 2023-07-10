using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Identity;
using Microsoft.Extensions.Primitives;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.User;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private readonly IAdminRepository _adminRepository;

    public UserController(
        IUserRepository userRepository,
        IAdminRepository adminRepository
    )
    {
        _userRepository = userRepository;
        _adminRepository = adminRepository;
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("create-address")]
    public async Task<IActionResult> RegisterAddress(
        [FromBody] CreteAddressRequest request
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _userRepository.CreateAddressAsync(headerValue!, request);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("read-users-in-range/{skip}/{take}")]
    public async Task<IActionResult> ReadUsersInRange(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbUsers = await _adminRepository.ReadUsersInRangeAsync(
                headerValue!,
                skip,
                take
            );
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("read-user-by-id/{id}")]
    public async Task<IActionResult> ReadUserById([FromRoute] int id)
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var _dbUser = await _adminRepository.ReadUserByIdAsync(headerValue!, id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet("read-my-addresses")]
    public async Task<IActionResult> ReadMyAddresses()
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            return Ok(await _userRepository.ReadMyAddressesAsync(headerValue!));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet("read-my-data")]
    public async Task<IActionResult> ReadMyData()
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            return Ok(await _userRepository.ReadMyDataAsync(headerValue!));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [Authorize(Policy = IdentityData.UserPolicyName)]
    // [HttpPost("cart-add-products")]
    // public async Task<IActionResult> AddProductsCart()
    // {
    //     try
    //     {
    //         Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpDelete("delete-my-addresses")]
    public async Task<IActionResult> DeleteMyAddresses()
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _userRepository.DeleteMyAddressesAsync(headerValue!);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpDelete("delete-my-account")]
    public async Task<IActionResult> DeleteMyAccount()
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _userRepository.DeleteMyAccountAsync(headerValue!);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-user-by-id-async/{id}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] int id)
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _adminRepository.DeleteUserByIdAsync(headerValue!, id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-users-in-range/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _adminRepository.DeleteUsersInRangeAsync(headerValue!, skip, take);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
