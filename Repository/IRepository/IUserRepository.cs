using Azure;
using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Repository.IRepository;

public interface IUserRepository 
{   
    Task<Response> authenticate([FromBody] LoginRequest request);

    Task<Response> register([FromBody] RegisterRequest request);

    // Task<IActionResult> update();

    // Task<IActionResult> delete();
}
