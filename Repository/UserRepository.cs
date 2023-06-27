using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Response;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;

    public UserRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserModel>> GetAllUsersAsync(int skip, int take)
    {
        var _dbUsers = await _dbContext.Users.Skip(skip).Take(take).ToListAsync();

        if (_dbUsers.Count == 0)
        {
            throw new NullReferenceException();
        }

        return _dbUsers;
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        var _dbUser = await _dbContext.Users.FindAsync(id);

        if (_dbUser == null)
        {
            throw new NullReferenceException();
        }

        return _dbUser;
    }

    public async Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take)
    {
        var _dbUsers = await GetAllUsersAsync(skip, take);

        if (_dbUsers.Count == 0)
        {
            throw new NullReferenceException();
        }

        _dbContext.Users.RemoveRange(_dbUsers);
        var _isSaved = await _dbContext.SaveChangesAsync();

        return _dbUsers;
    }

    public async Task<UserModel> DeleteUserByIdAsync(int id)
    {
        var _dbUser = await GetUserByIdAsync(id);

        if (_dbUser == null)
        {
            throw new NullReferenceException();
        }

        _dbContext.Users.Remove(_dbUser);
        await _dbContext.SaveChangesAsync();

        return _dbUser;
    }

    private async Task<UserModel> GetUser(JwtRequest request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        if (!jwtSecurityTokenHandler.CanReadToken(request.Token))
        {
            throw new Exception();
        }

        var token = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

        if (!(token.ValidTo >= DateTime.Now))
        {
            throw new Exception();
        }

        var dbUser =
            await _dbContext.Users
                .Include(p => p.Cart)
                .ThenInclude(p => p.Orders)
                .Include(p => p.Addresses)
                .FirstOrDefaultAsync(p => p.Id == 1)
            ?? throw new NullReferenceException();

        return dbUser;
    }

    public async Task<UserResponse> GetMyDataAsync(JwtRequest request)
    {
        var dbUser = await GetUser(request);

        List<AddressResponse> addresses = new List<AddressResponse>();

        foreach (var address in dbUser.Addresses)
        {
            addresses.Add(
                new AddressResponse()
                {
                    Address = address.Address,
                    Number = address.Number,
                    District = address.District,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                }
            );
        }

        var response = new UserResponse()
        {
            Name = $"{dbUser.Firstname} {dbUser.Lastname}",
            Email = dbUser.Email,
            Phone = dbUser.Phone,
            Cart = new CartResponse()
            {
                Id = dbUser.Cart.Id,
                Orders = new List<OrderResponse>() { },
            },
            Addresses = addresses
        };

        return response;
    }

    public async Task RegisterAddressAsync(
        JwtRequest jwtRequest,
        AddressRequest addressRequest
    )
    {
        var dbUser = await GetUser(jwtRequest);
        dbUser.Addresses.Add(
            new AddressModel()
            {
                Address = addressRequest.Address,
                Number = addressRequest.Number,
                ZipCode = addressRequest.ZipCode,
                District = addressRequest.District,
                City = addressRequest.City,
                State = addressRequest.State
            }
        );

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePasswordUserAsync(
        ForgottenPasswordRequest forgottenPasswordRequest,
        JwtRequest jwtRequest
    )
    {
        // var dbUser = await GetUserByIdAsync(id);

        // var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        // request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        // dbUser.Password = request.NewPassword;
        // dbUser.UpdatedAt = DateTime.Today;
        // await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMyAccountAsync()
    {
        throw new NotImplementedException();
    }
}
