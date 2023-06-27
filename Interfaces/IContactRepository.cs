using kazariobranco_backend.Models;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IContactRepository
{
    Task<List<ContactModel>> GetAllContactsAsync(int skip, int take);

    Task<ContactModel> GetContactByIdAsync(int id);

    Task CreateContactAsync(ContactRequest request);

    Task<ContactModel> UpdateStatusByIdAsync(int id);

    Task<List<ContactModel>> UpdateAllStatusAsync(int skip, int take);

    Task<ContactModel> DeleteContactByIdAsync(int id);

    Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take);
}
