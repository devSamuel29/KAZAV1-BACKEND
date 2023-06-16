using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Response;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{

    private readonly MyDbContext _dbContext;

    public UserRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponse> GetMyDataAsync(JwtRequest request)
    {

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        if(!jwtSecurityTokenHandler.CanReadToken(request.Token)) {
            throw new Exception();
        }
        var token = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        
        var dbUser = await _dbContext.Users
            .Include(p => p.Cart)
            .ThenInclude(p => p.Orders)
            .Include(p => p.Addresses)
            .FirstOrDefaultAsync();

        if (dbUser is null)
        {
            throw new NullReferenceException();
        }

        var response = new UserResponse()
        {
            Name = $"{dbUser.Firstname} {dbUser.Lastname}",
            Email = dbUser.Email,
            Phone = dbUser.Phone,
            Cart = new CartResponse()
            {
                Id = dbUser.Cart.Id,
                Orders = new List<OrderResponse>() {  },
            },
            Addresses = new List<AddressResponse>() { },
        };

        return response;
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

    // public async Task RegisterAddressAsync(AddressRequest request, int id, string email)
    // {
    //     var dbUser = await GetMyDataAsync(id, email);

    //     await _addressRepository.AddAddressAsync(request, dbUser);
    // }

    public async Task DeleteMyAccountAsync()
    {
        throw new NotImplementedException();
    }
}
