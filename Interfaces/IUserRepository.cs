using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;
using System.Collections.Generic;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    
    Task<List<UserModel>> GetAllUsersAsync(int skip, int take);

    Task<UserModel> GetUserByIdAsync(int id);

    Task<Response> Authenticate(LoginRequest request);

    Task<Response> Register(RegisterRequest request);

    Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request);

    Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take);

    Task<UserModel> DeleteUserByIdAsync(int id);
}
