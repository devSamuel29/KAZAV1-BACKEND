using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using System.IdentityModel.Tokens.Jwt;

namespace kazariobranco_backend.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly MyDbContext _dbContext;

    public AddressRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<AddressModel>> GetMyAddresses(int id)
    {
        return await _dbContext.Addresses.Where(p => p.UserId == id).ToListAsync();
    }

    public async Task AddAddressAsync(JwtRequest jwt, AddressRequest request)
    {
        // user.Addresses.Add(
        //     new AddressModel()
        //     {
        //         Address = request.Address,
        //         Number = request.Number,
        //         ZipCode = request.ZipCode,
        //         District = request.District,
        //         City = request.City,
        //         State = request.State
        //     }
        // );

        await _dbContext.SaveChangesAsync();
    }

    public string ReadToken(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();

        var jsonToken = handler.ReadToken(jwt);
        return jsonToken.ToString();
    }
}
