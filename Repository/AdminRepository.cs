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

    public async Task<UserResponse> ReadUserByIdAsync(string token, int id)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbUser =
            await _dbContext.Users.FindAsync(id)
            ?? throw new NullReferenceException("Usuário não encontrado!");

        return _mapper.Map<UserResponse>(dbUser);
    }

    public async Task<IList<UserResponse>> ReadUsersInRangeAsync(
        string token,
        int skip,
        int take
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbUsers = await _dbContext.Users
            .Skip(skip)
            .Take(take)
            .Where(p => p.Role != IdentityData.AdminClaimName)
            .AsNoTracking()
            .ToListAsync();

        if (!dbUsers.Any())
        {
            throw new NullReferenceException("Nenhum usuário registrado no banco!");
        }

        return _mapper.Map<IList<UserResponse>>(dbUsers);
    }

    public async Task DeleteUserByIdAsync(string token, int id)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbUser =
            await _dbContext.Users.FindAsync(id)
            ?? throw new NullReferenceException("Usuário não encontrado");

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUsersInRangeAsync(string token, int skip, int take)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbUsers = _dbContext.Users.Skip(skip).Take(take).AsNoTracking();

        if (!dbUsers.Any())
        {
            throw new NullReferenceException("Nenhum usuário registrado no banco!");
        }

        _dbContext.Users.RemoveRange(dbUsers);
        await _dbContext.SaveChangesAsync();
    }

    // ADMIN-CONTACT

    public async Task<ContactResponse> ReadContactByIdAsync(string token, int id)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContact =
            await _dbContext.Contacts.FindAsync(id)
            ?? throw new NullReferenceException("Usuário não encontrado!");

        return _mapper.Map<ContactResponse>(dbContact);
    }

    public async Task<IList<ContactResponse>> ReadContactsByEmailAsync(
        string token,
        string email
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Email.StartsWith(email))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<IList<ContactResponse>> ReadContactsByNameAsync(
        string token,
        string name
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Name.StartsWith(name))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<IList<ContactResponse>> ReadContactsByPhoneAsync(
        string token,
        string phone
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Phone.StartsWith(phone))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task<ReadAllContactsResponse> ReadContactsInRangeAsync(
        string token,
        int skip,
        int take,
        bool? orderByDate
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        if (skip > take)
        {
            return await AllContactsAsync(take, skip);
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

    public async Task<ContactResponse> UpdateStatusByIdAsync(string token, int id)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContact =
            await _dbContext.Contacts.FindAsync(id)
            ?? throw new NullReferenceException("Usuário não encontrado!");

        if (dbContact.EndedAt != DateTime.MinValue)
        {
            throw new InvalidOperationException("Contato já finalizado!");
        }

        dbContact.EndedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ContactResponse>(dbContact);
    }

    public async Task<IList<ContactResponse>> UpdateStatusInRangeAsync(
        string token,
        int skip,
        int take
    )
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContacts = await _dbContext.Contacts.Skip(skip).Take(take).ToListAsync();

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

        dbContacts.ForEach(p => p.EndedAt = DateTime.Now);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<IList<ContactResponse>>(dbContacts);
    }

    public async Task DeleteContactByIdAsync(string token, int id)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContact =
            await _dbContext.Contacts.FindAsync(id)
            ?? throw new NullReferenceException("Usuário não encontrado!");

        _dbContext.Contacts.Remove(dbContact);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteContactsInRangeAsync(string token, int skip, int take)
    {
        Claims adminJwt = await GetClaims(token);

        await _dbContext.Users.FirstAsync(
            p => p.Id == adminJwt.Id && p.Role == adminJwt.Role
        );

        var dbContacts = _dbContext.Contacts.Skip(skip).Take(take).AsNoTracking();

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

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

        if (!dbContacts.Any())
        {
            throw new NullReferenceException("Nenhum contato registrado no banco!");
        }

        var mappedContacts = _mapper.Map<IList<ContactResponse>>(dbContacts);

        var response = new ReadAllContactsResponse()
        {
            Size = await _dbContext.Contacts.CountAsync(),
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

            if (!dbContacts.Any())
            {
                throw new NullReferenceException("Nenhum contato registrado no banco!");
            }

            var mappedContacts = _mapper.Map<IList<ContactResponse>>(dbContacts);

            var response = new ReadAllContactsResponse()
            {
                Size = await _dbContext.Contacts.CountAsync(),
                From = skip,
                To = take,
                Quantity = dbContacts.Count,
                Contacts = mappedContacts
            };

            return response;
        }

        return await AllContactsAsync(skip, take);
    }

    private async Task<Claims> GetClaims(string token)
    {
        token = await _jwtService.FormatToken(token);
        return await _jwtService.GetClaims(token);
    }
}
