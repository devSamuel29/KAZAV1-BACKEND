using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Interfaces;

public interface IContactRepository
{
    Task<Response> createContactOrder([FromBody] ContactRequest request);
}
