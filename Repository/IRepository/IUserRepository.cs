using Azure;
using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Repository.IRepository;

public interface IUserRepository 
{   
    Task<IActionResult> authenticate([FromBody] LoginRequest request);

    Task<IActionResult> register([FromBody] RegisterRequest request);

    // Task<IActionResult> update();

    // Task<IActionResult> delete();
}
