using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Response;
using kazariobranco_backend.Validator;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Contact;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IMapper _mapper;

    public ContactRepository(MyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validate = await validator.ValidateAsync(request);

        if (validate.IsValid)
        {
            await _dbContext.Contacts.AddAsync(_mapper.Map<ContactModel>(request));
            await _dbContext.SaveChangesAsync();
            return;
        }

        throw new InvalidDataException(validate.ToString());
    }

    public async Task<ContactResponse> ReadContactByIdAsync(int id)
    {
        var dbContact = await _dbContext.Contacts.FindAsync(id);

        if (dbContact is null)
        {
            throw new NullReferenceException("id inexistente");
        }

        return _mapper.Map<ContactResponse>(dbContact);
    }

    public async Task<IList<ContactResponse>> ReadContactsByNameAsync(string name)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Name.StartsWith(name))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IList<ContactResponse>>(dbContacts);
        return response;
    }

    public async Task<IList<ContactResponse>> ReadContactsByEmailAsync(string email)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Email.StartsWith(email))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IList<ContactResponse>>(dbContacts);

        return response;
    }

    public async Task<IList<ContactResponse>> ReadContactsByPhoneAsync(string phone)
    {
        var dbContacts = await _dbContext.Contacts
            .Where(p => p.Phone.StartsWith(phone))
            .OrderBy(p => p.CreatedAt)
            .Reverse()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IList<ContactResponse>>(dbContacts);

        return response;
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

    // public async Task DeleteAllContactsAsync()
    // {
    //     var dbContacts = _dbContext.Contacts.Skip(skip).Take(take).AsNoTracking();
    //     _dbContext.Contacts.Remove();
    // }

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
