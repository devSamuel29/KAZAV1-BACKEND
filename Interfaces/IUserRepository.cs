using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task CreateAddressAsync(string token, AddNewAddressRequest request);

    Task<UserResponse> ReadUserByIdAsync(int id);

    Task<IList<UserResponse>> ReadUsersInRangeAsync(int skip, int take);

    Task<IList<AddressResponse>> ReadMyAddressesAsync(string token);

    Task<UserResponse> ReadMyDataAsync(string token);

    Task UpdatePasswordUserAsync();

    Task DeleteMyAccountAsync(string token);

    Task DeleteUserByIdAsync(int id);

    Task DeleteUsersInRangeAsync(int skip, int take);
}
