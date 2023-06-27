using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllUsersAsync(int skip, int take);

    Task<UserModel> GetUserByIdAsync(int id);

    Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take);

    Task<UserModel> DeleteUserByIdAsync(int id);
    
    Task<UserResponse> GetMyDataAsync(JwtRequest request);

    Task UpdatePasswordUserAsync(
        ForgottenPasswordRequest forgottenPasswordRequest,
        JwtRequest jwtRequest
    );

    Task RegisterAddressAsync(JwtRequest jwtRequest, AddressRequest addressRequest);

    Task DeleteMyAccountAsync();
}
