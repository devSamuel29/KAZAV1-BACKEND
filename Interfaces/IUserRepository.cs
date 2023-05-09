using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository 
{   
    Task<IActionResult> authenticate([FromBody] LoginRequest request);

    Task<IActionResult> register([FromBody] RegisterRequest request);

    // Task<IActionResult> update();

    // Task<IActionResult> delete();
}
