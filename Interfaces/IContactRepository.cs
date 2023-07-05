using kazariobranco_backend.Request.Contact;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IContactRepository
{
    Task CreateContactAsync(ContactRequest request);

    Task<IList<ContactResponse>> ReadContactsByNameAsync(string name);

    Task<IList<ContactResponse>> ReadContactsByEmailAsync(string email);

    Task<IList<ContactResponse>> ReadContactsByPhoneAsync(string phone);

    Task<ReadAllContactsResponse> ReadContactsInRangeAsync(int skip, int take, bool? byDate);

    Task<ContactResponse> ReadContactByIdAsync(int id);

    Task<ContactResponse> UpdateStatusByIdAsync(int id);

    Task<IList<ContactResponse>> UpdateStatusInRangeAsync(int skip, int take);

    Task DeleteContactByIdAsync(int id);

    Task DeleteContactsInRangeAsync(int skip, int take);
}
