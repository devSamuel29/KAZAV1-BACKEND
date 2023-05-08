using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Repository.IRepository;

public interface IContactRepository
{
    Task<IActionResult> createContactOrder([FromBody] ContactRequest request);
}
