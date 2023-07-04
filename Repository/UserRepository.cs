using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Response;
using kazariobranco_backend.Models;
using kazariobranco_backend.Identity;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IMapper _mapper;

    private readonly IJwtService _jwtService;

    public UserRepository(MyDbContext dbContext, IMapper mapper, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task CreateAddressAsync(string token, AddNewAddressRequest request)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await _dbContext.Users
            .Include(p => p.Addresses)
            .FirstAsync(p => p.Id == claims.Id && p.Email == claims.Email);

        if (dbUser.Addresses.Count > 2)
        {
            throw new Exception("opa nao pode mais");
        }
        else if (dbUser.Email == claims.Email)
        {
            dbUser.Addresses.Add(_mapper.Map<AddressModel>(request));
            await _dbContext.SaveChangesAsync();
            return;
        }

        throw new Exception("sla");
    }

    public async Task CreateProductsCartAsync(
        string token,
        AddProductsCartRequest request
    ) { }

    public async Task<UserResponse> ReadUserByIdAsync(int id)
    {
        var dbUser =
            await _dbContext.Users.FindAsync(id) ?? throw new NullReferenceException();

        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task<IList<UserResponse>> ReadUsersInRangeAsync(int skip, int take)
    {
        var dbUsers =
            await _dbContext.Users.Skip(skip).Take(take).AsNoTracking().ToListAsync()
            ?? throw new NullReferenceException();

        return _mapper.Map<IList<UserResponse>>(dbUsers);
    }

    public async Task<IList<AddressResponse>> ReadMyAddressesAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await _dbContext.Users
            .Include(p => p.Addresses)
            .AsNoTracking()
            .FirstAsync(p => p.Id == claims.Id && p.Email == claims.Email);

        IList<AddressResponse> response = new List<AddressResponse>();
        foreach (var address in dbUser!.Addresses)
        {
            response.Add(_mapper.Map<AddressResponse>(address));
        }

        return response;
    }

    public async Task<UserResponse> ReadMyDataAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await ReadUserByIdAsync(claims.Id);
        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task UpdatePasswordUserAsync()
    {
        // var dbUser = await GetUserByIdAsync(id);

        // var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        // request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        // dbUser.Password = request.NewPassword;
        // dbUser.UpdatedAt = DateTime.Today;
        // await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMyAccountAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser =
            await _dbContext.Users.FindAsync(claims.Id)
            ?? throw new NullReferenceException("asddsa");

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserByIdAsync(int id)
    {
        var dbUser =
            await _dbContext.Users.FindAsync(id)
            ?? throw new NullReferenceException("asddsa");

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUsersInRangeAsync(int skip, int take)
    {
        var dbUsers =
            _dbContext.Users.Skip(skip).Take(take).AsNoTracking()
            ?? throw new Exception("sei la");

        _dbContext.Users.RemoveRange(dbUsers);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Claims> GetClaims(string token)
    {
        token = await _jwtService.FormatToken(token);
        return await _jwtService.GetClaims(token);
    }
}
