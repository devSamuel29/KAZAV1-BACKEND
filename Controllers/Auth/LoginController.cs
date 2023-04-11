using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using kazariobranco_backend.Request;
using Microsoft.IdentityModel.Tokens;
using kazariobranco_backend.Models;
using System.Text;

namespace kazariobranco_backend.Controllers.Auth;

[ApiController]
[Route("api/auth/[controller]")]
public class LoginController : ControllerBase
{
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        this._config = config;
    }

    // private string GenerateToken(User user) {
    //     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    //     return securityKey;
    // }

    // private UserRequest Authenticate(UserRequest userRequest {

    // }

    // [AllowAnonymous] 
    [HttpGet]
    public IActionResult Login([FromBody] UserRequest userRequest)
    {
        // var token = GenerateToken();
        // var user = Authenticate(token);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));


        return Ok();
        // return NotFound("Usuário não encontrado");
    }
}
