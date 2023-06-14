using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IConfiguration _config;

    private readonly IAddressRepository _addressRepository;

    public UserRepository(MyDbContext dbContext, IAddressRepository addressRepository)
    {
        _dbContext = dbContext;
        _addressRepository = addressRepository;
    }

    public async Task<UserModel> GetMyDataAsync(int id, string email)
    {
        var dbUser = await _dbContext.Users
            .Include(p => p.Cart)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (dbUser is null)
        {
            throw new NullReferenceException();
        }
        else if (dbUser.Email != email)
        {
            throw new InvalidOperationException();
        }

        return dbUser;
    }

    public async Task UpdatePasswordUserAsync(ForgottenPasswordRequest request, int id)
    {
        // var dbUser = await GetUserByIdAsync(id);

        // var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        // request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        // dbUser.Password = request.NewPassword;
        // dbUser.UpdatedAt = DateTime.Today;
        // await _dbContext.SaveChangesAsync();
    }

    public async Task RegisterAddressAsync(AddressRequest request, int id, string email)
    {
        var dbUser = await GetMyDataAsync(id, email);

        await _addressRepository.AddAddressAsync(request, dbUser);
    }

    public async Task DeleteMyAccountAsync()
    {
        throw new NotImplementedException();
    }
}
