using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;
using System.Collections.Generic;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<Response> Authenticate(LoginRequest request);

    Task<Response> Register(RegisterRequest request);

    Task<List<UserModel>> GetAllUsersAsync();

    Task<UserModel> GetUserByIdAsync(int id);

    Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request);

    Task<List<UserModel>> DeleteAllUsersAsync();

    Task<UserModel> DeleteUserByIdAsync(int id);
}
