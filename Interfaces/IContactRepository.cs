using kazariobranco_backend.Request.Contact;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IContactRepository
{
    Task CreateContactAsync(ContactRequest request);
}
