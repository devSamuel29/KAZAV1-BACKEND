using kazariobranco_backend.Models;

namespace kazariobranco_backend.Interfaces;

public interface IAdminManageUsersRepository
{
    Task<List<UserModel>> GetAllUsersAsync(int skip, int take);

    Task<UserModel> GetUserByIdAsync(int id);

    Task RegisterAdminAsync();

    Task LoginAsAdminAsync();

    Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take);

    Task<UserModel> DeleteUserByIdAsync(int id);
}
