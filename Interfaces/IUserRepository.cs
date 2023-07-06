using kazariobranco_backend.Request.User;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task CreateAddressAsync(string token, CreteAddressRequest request);

    Task<IList<AddressResponse>> ReadMyAddressesAsync(string token);

    Task<UserResponse> ReadMyDataAsync(string token);

    Task UpdatePasswordUserAsync(string email);

    Task DeleteMyAddressByIdAsync(string token, int id);

    Task DeleteMyAddressesAsync(string token);

    Task DeleteMyAccountAsync(string token);
}
