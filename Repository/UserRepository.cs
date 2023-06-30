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

    public async Task<List<UserModel>> GetAllUsersAsync(int skip, int take)
    {
        var dbUsers = await _dbContext.Users
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();

        if (dbUsers is null)
        {
            throw new NullReferenceException();
        }

        return dbUsers;
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        var dbUser = await _dbContext.Users.FindAsync(id);

        if (dbUser is null)
        {
            throw new NullReferenceException();
        }

        return dbUser;
    }

    public async Task<IList<AddressResponse>> ListMyAddressesAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await _dbContext.Users
            .Include(p => p.Addresses)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == claims.Id && p.Email == claims.Email);

        IList<AddressResponse> response = new List<AddressResponse>();
        foreach (var address in dbUser.Addresses)
        {
            response.Add(_mapper.Map<AddressResponse>(address));
        }

        return await Task.FromResult(response);
    }

    public async Task<UserResponse> MyDataAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await GetUserByIdAsync(claims.Id);
        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task RegisterAddressAsync(string token, AddNewAddressRequest request)
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

        //mudar a exception
        throw new Exception("sla");
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

        var dbUser = await _dbContext.Users.FirstAsync(
            p => p.Id == claims.Id && p.Email == claims.Email
        );

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();
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

    private async Task<Claims> GetClaims(string token)
    {
        token = await _jwtService.FormatToken(token);
        return await _jwtService.GetClaims(token);
    }
}
