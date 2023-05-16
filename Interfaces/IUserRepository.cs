using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;
using System.Text.Json.Nodes;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository 
{   
    Task<Response> authenticate([FromBody] LoginRequest request);

    Task<Response> register([FromBody] RegisterRequest request);

    // Task<string> update();

    // Task<string> delete();
}
