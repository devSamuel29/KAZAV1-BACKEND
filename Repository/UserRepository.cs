using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Response;
using kazariobranco_backend.Interfaces;

using Microsoft.EntityFrameworkCore;

using AutoMapper;
using kazariobranco_backend.Request.User;

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

    public async Task CreateAddressAsync(string token, CreteAddressRequest request)
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
    }

    public async Task CreateProductsCartAsync(
        string token,
        AddProductsCartRequest request
    ) { }

    public async Task<IList<AddressResponse>> ReadMyAddressesAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await _dbContext.Users
            .Include(p => p.Addresses)
            .AsNoTracking()
            .FirstAsync(p => p.Id == claims.Id && p.Email == claims.Email);

        IList<AddressResponse> response = _mapper.Map<IList<AddressResponse>>(
            dbUser.Addresses.ToList()
        );

        if (!response.Any())
        {
            throw new Exception(
                $"Não há endereços cadastrados para o usuário {dbUser.Firstname} {dbUser.Lastname}!"
            );
        }
        return _mapper.Map<IList<AddressResponse>>(dbUser.Addresses.ToList());
    }

    public async Task<UserResponse> ReadMyDataAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUser = await _dbContext.Users.FirstAsync(
            p => p.Id == claims.Id && p.Email == claims.Email
        );

        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task UpdatePasswordUserAsync(string email)
    {
        
    }

    public async Task DeleteMyAddressByIdAsync(string token, int id)
    {
        Claims claims = await GetClaims(token);

        var dbUserAddress = await _dbContext.Addresses.FirstAsync(
            p => p.UserId == claims.Id && p.Id == id
        );

        _dbContext.Addresses.Remove(dbUserAddress);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMyAddressesAsync(string token)
    {
        Claims claims = await GetClaims(token);

        var dbUserAddresses =
            await _dbContext.Addresses.Where(p => p.UserId == claims.Id).ToListAsync()
            ?? throw new NullReferenceException("asddsa");

        _dbContext.Addresses.RemoveRange(dbUserAddresses);
        await _dbContext.SaveChangesAsync();
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

    private async Task<Claims> GetClaims(string token)
    {
        token = await _jwtService.FormatToken(token);
        return await _jwtService.GetClaims(token);
    }
}
