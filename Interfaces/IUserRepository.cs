using Microsoft.AspNetCore.Mvc;
using kazariobranco_backend.Request;
using kazariobranco_backend.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace kazariobranco_backend.Interfaces;

public interface IUserRepository
{
    Task<UserModel> GetMyDataAsync(int id, string email);

    Task UpdatePasswordUserAsync(ForgottenPasswordRequest request, int id);

    Task RegisterAddressAsync(AddressRequest request, int id, string email);

    Task DeleteMyAccountAsync();
}
