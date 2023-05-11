using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository 
{   
    // Task<string> authenticate([FromBody] LoginRequest request);

    Task<string> register([FromBody] RegisterRequest request);

    // Task<string> update();

    // Task<string> delete();
}
