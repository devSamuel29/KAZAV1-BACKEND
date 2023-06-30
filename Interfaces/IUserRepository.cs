using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllUsersAsync(int skip, int take);

    Task<UserModel> GetUserByIdAsync(int id);

    Task<UserResponse> MyDataAsync(string token);

    Task RegisterAddressAsync(string token, AddNewAddressRequest request);

    Task UpdatePasswordUserAsync(JwtRequest jwtRequest);

    Task DeleteMyAccountAsync(JwtRequest jwtRequest);

    Task<UserModel> DeleteUserByIdAsync(int id);

    Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take);
}
