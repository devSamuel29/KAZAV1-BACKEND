using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository 
{   
    // Task<string> authenticate([FromBody] LoginRequest request);

    Task<Response> register([FromBody] RegisterRequest request);

    // Task<string> update();

    // Task<string> delete();
}
