using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IConfiguration _config;

    public Task<UserModel> GetMyData()
    {
        throw new NotImplementedException();
    }
    
    public UserRepository(IConfiguration config, MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _config = config;
    }
    
    public async Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request)
    {
        // var dbUser = await GetUserByIdAsync(id);

        // var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        // request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        // dbUser.Password = request.NewPassword;
        // dbUser.UpdatedAt = DateTime.Today;
        // await _dbContext.SaveChangesAsync();

        return new Response(200, "sucess");
    }


    public Task DeleteMyAccount()
    {
        throw new NotImplementedException();
    }
}
