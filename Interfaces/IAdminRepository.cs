using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IAdminRepository
{
    // ADMIN-USER

    Task<UserResponse> ReadUserByIdAsync(string token, int id);

    Task<IList<UserResponse>> ReadUsersInRangeAsync(string token, int skip, int take);

    Task DeleteUserByIdAsync(string token, int id);

    Task DeleteUsersInRangeAsync(string token, int skip, int take);

    // ADMIN-CONTACT

    Task<ContactResponse> ReadContactByIdAsync(string token, int id);

    Task<IList<ContactResponse>> ReadContactsByNameAsync(string token, string name);

    Task<IList<ContactResponse>> ReadContactsByEmailAsync(string token, string email);

    Task<IList<ContactResponse>> ReadContactsByPhoneAsync(string token, string phone);

    Task<ContactResponse> UpdateStatusByIdAsync(string token, int id);

    Task<IList<ContactResponse>> UpdateStatusInRangeAsync(
        string token,
        int skip,
        int take
    );

    Task<ReadAllContactsResponse> ReadContactsInRangeAsync(
        string token,
        int skip,
        int take,
        bool? byDate
    );

    Task DeleteContactByIdAsync(string token, int id);

    Task DeleteContactsInRangeAsync(string token, int skip, int take);
}
