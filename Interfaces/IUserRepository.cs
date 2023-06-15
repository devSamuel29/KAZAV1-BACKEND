using kazariobranco_backend.Request;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<UserResponse> GetMyDataAsync(string jwt);

    Task UpdatePasswordUserAsync(ForgottenPasswordRequest request, int id);

    // Task RegisterAddressAsync(AddressRequest request, int id, string email);

    Task DeleteMyAccountAsync();
}
