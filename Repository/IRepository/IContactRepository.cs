using Azure;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Repository.IRepository;

public interface IContactRepository
{
    Task<Response> createContactOrder([FromBody] ContactRequest request);
}
