namespace kazariobranco_backend.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;

public class AdminToUserRepository : IAdminToUserRepository
{
    private readonly MyDbContext _dbContext;

    public AdminToUserRepository(MyDbContext dbContext)
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

    public Task LoginAsAdminAsync()
    {
        throw new NotImplementedException();
    }

    public Task RegisterAdminAsync()
    {
        throw new NotImplementedException();
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
}
