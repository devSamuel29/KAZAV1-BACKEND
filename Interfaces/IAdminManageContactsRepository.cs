using kazariobranco_backend.Models;

namespace kazariobranco_backend.Interfaces;

public interface IAdminManageContactsRepository 
{ 
    Task<List<ContactModel>> GetAllContactsAsync(int skip, int take);

    Task<ContactModel> GetContactByIdAsync(int id);

    Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take);

    Task<ContactModel> UpdateStatusByIdAsync(int id);

    Task<List<ContactModel>> UpdateAllStatusAsync(int skip, int take);

    Task<ContactModel> DeleteContactByIdAsync(int id);
}
