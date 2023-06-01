using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<UserModel> GetMyData();

    Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request);

    Task DeleteMyAccount();
}
