using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IAdminRepository
{
    // ADMIN-USER

    Task<UserResponse> ReadUserByIdAsync(int id);

    Task<IList<UserResponse>> ReadUsersInRangeAsync(int skip, int take);

    Task DeleteUserByIdAsync(int id);

    Task DeleteUsersInRangeAsync(int skip, int take);

    // ADMIN-CONTACT

    Task<ContactResponse> ReadContactByIdAsync(int id);

    Task<IList<ContactResponse>> ReadContactsByNameAsync(string name);

    Task<IList<ContactResponse>> ReadContactsByEmailAsync(string email);

    Task<IList<ContactResponse>> ReadContactsByPhoneAsync(string phone);

    Task<ContactResponse> UpdateStatusByIdAsync(int id);

    Task<IList<ContactResponse>> UpdateStatusInRangeAsync(int skip, int take);

    Task<ReadAllContactsResponse> ReadContactsInRangeAsync(
        int skip,
        int take,
        bool? byDate
    );

    Task DeleteContactByIdAsync(int id);

    Task DeleteContactsInRangeAsync(int skip, int take);
}
