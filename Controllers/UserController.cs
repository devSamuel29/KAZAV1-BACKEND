using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using kazariobranco_backend.Request;
using kazariobranco_backend.Identity;
using Microsoft.Extensions.Primitives;
using kazariobranco_backend.Interfaces;

namespace kazariobranco_backend.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("add-address")]
    public async Task<IActionResult> RegisterAddress(
        [FromBody] AddNewAddressRequest request
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
    [HttpGet("get-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> GetAllUsersAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            var _dbUsers = await _userRepository.ReadUsersInRangeAsync(skip, take);
            return Ok(_dbUsers);
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet("get-user-by-id-async/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        try
        {
            var _dbUser = await _userRepository.ReadUserByIdAsync(id);
            return Ok(_dbUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet("get-my-addresses")]
    public async Task<IActionResult> ListMyAddresses()
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
    [HttpGet("my-data")]
    public async Task<IActionResult> MyDataAsync()
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
    [HttpDelete("delete-all-addresses")]
    public async Task<IActionResult> DeleteMyAddresses()
    {
        try
        {
            Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            await _userRepository.DeleteMyAddressAsync(headerValue!);
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
    public async Task<IActionResult> DeleteUserByIdAsync([FromRoute] int id)
    {
        try
        {
            await _userRepository.DeleteUserByIdAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete("delete-all-users-async/{skip}/{take}")]
    public async Task<IActionResult> DeleteAllUsersAsync(
        [FromRoute] int skip,
        [FromRoute] int take
    )
    {
        try
        {
            await _userRepository.DeleteUsersInRangeAsync(skip, take);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
