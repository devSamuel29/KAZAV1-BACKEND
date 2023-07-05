using AutoMapper;
using kazariobranco_backend.Database;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Response;
using Microsoft.EntityFrameworkCore;

namespace kazariobranco_backend.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly IMapper _mapper;

    private readonly MyDbContext _dbContext;

    private readonly IJwtService _jwtService;

    public AdminRepository(IMapper mapper, MyDbContext dbContext, IJwtService jwtService)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    // ADMIN-USER

    public async Task<UserResponse> ReadUserByIdAsync(int id)
    {
        var dbUser =
            await _dbContext.Users.FindAsync(id) ?? throw new NullReferenceException();

        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task<IList<UserResponse>> ReadUsersInRangeAsync(int skip, int take)
    {
        var dbUsers =
            await _dbContext.Users
                .Skip(skip)
                .Take(take)
                .Where(p => p.Role != IdentityData.AdminClaimName)
                .AsNoTracking()
                .ToListAsync() ?? throw new NullReferenceException();

        return _mapper.Map<IList<UserResponse>>(dbUsers);
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

    // ADMIN-CONTACT

    public async Task<ContactResponse> ReadContactByIdAsync(int id)
    {
        var dbContact = await _dbContext.Contacts.FindAsync(id);

        if (dbContact is null)
        {
            throw new NullReferenceException("id inexistente");
        }

        return _mapper.Map<ContactResponse>(dbContact);
    }

    public async Task<IList<ContactResponse>> ReadContactsByEmailAsync(string email)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Email.StartsWith(email))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<IList<ContactResponse>> ReadContactsByNameAsync(string name)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Name.StartsWith(name))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<IList<ContactResponse>> ReadContactsByPhoneAsync(string phone)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Phone.StartsWith(phone))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<ReadAllContactsResponse> ReadContactsInRangeAsync(
        int skip,
        int take,
        bool? orderByDate
    )
    {
        if (skip > take || skip == take)
        {
            throw new Exception("ainda n sei oq colocar");
        }
        else if (orderByDate.HasValue)
        {
            if (orderByDate.Value)
            {
                return await AllContactsByDateAsync(skip, take, orderByDate.Value);
            }
        }

        return await AllContactsAsync(skip, take);
    }

    public async Task<ContactResponse> UpdateStatusByIdAsync(int id)
    {
        var dbContact = await _dbContext.Contacts.FindAsync(id);

        if (dbContact is null)
        {
            throw new Exception("sei la");
        }

        dbContact.EndedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ContactResponse>(dbContact);
    }

    public async Task<IList<ContactResponse>> UpdateStatusInRangeAsync(int skip, int take)
    {
        var dbContacts = await _dbContext.Contacts.Skip(skip).Take(take).ToListAsync();

        if (dbContacts is null)
        {
            throw new Exception("sei la");
        }
        dbContacts.ForEach(p => p.EndedAt = DateTime.Now);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task DeleteContactByIdAsync(int id)
    {
        var dbContact = await _dbContext.Contacts.FindAsync(id);

        if (dbContact is null)
        {
            throw new NullReferenceException("id inexistente");
        }

        _dbContext.Contacts.Remove(dbContact);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteContactsInRangeAsync(int skip, int take)
    {
        var dbContacts =
            _dbContext.Contacts.Skip(skip).Take(take).AsNoTracking()
            ?? throw new Exception("sei la");

        _dbContext.Contacts.RemoveRange(dbContacts);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<ReadAllContactsResponse> AllContactsAsync(int skip, int take)
    {
        var dbContacts = await _dbContext.Contacts
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();

        var mappedContacts = _mapper.Map<IList<ContactResponse>>(dbContacts);

        var response = new ReadAllContactsResponse()
        {
            Size = _dbContext.Contacts.Count(),
            From = skip,
            To = take,
            Quantity = dbContacts.Count,
            Contacts = mappedContacts
        };

        return response;
    }

    private async Task<ReadAllContactsResponse> AllContactsByDateAsync(
        int skip,
        int take,
        bool orderByDate
    )
    {
        if (orderByDate)
        {
            var dbContacts = await _dbContext.Contacts
                .Skip(skip)
                .Take(take)
                .OrderBy(p => p.CreatedAt)
                .Reverse()
                .AsNoTracking()
                .ToListAsync();

            var mappedContacts = _mapper.Map<IList<ContactResponse>>(dbContacts);

            var response = new ReadAllContactsResponse()
            {
                Size = _dbContext.Contacts.Count(),
                From = skip,
                To = take,
                Quantity = dbContacts.Count,
                Contacts = mappedContacts
            };

            return response;
        }

        return await AllContactsAsync(skip, take);
    }
}
