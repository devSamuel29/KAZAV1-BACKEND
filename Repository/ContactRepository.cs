using AutoMapper;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Validator;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<ContactModel>> GetAllContactsAsync(int skip, int take)
    {
        var _dbContacts = await _dbContext.Contacts
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .ToListAsync();
        
        if (_dbContacts is null)
        {
            throw new NullReferenceException();
        }

        return _dbContacts;
    }

    public async Task<ContactModel> GetContactByIdAsync(int id)
    {
        var _dbContact = await _dbContext.Contacts
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (_dbContact is null)
        {
            throw new NullReferenceException();
        }

        return _dbContact;
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

    public async Task<List<ContactModel>> UpdateAllStatusAsync(int skip, int take)
    {
        var _dbContacts = await GetAllContactsAsync(skip, take);

        if (_dbContacts is null)
        {
            throw new NullReferenceException();
        }

        _dbContacts.ForEach(u => u.EndedAt = DateTime.Today);

        _dbContext.UpdateRange(_dbContacts);
        await _dbContext.SaveChangesAsync();

        return _dbContacts;
    }

    public async Task<ContactModel> UpdateStatusByIdAsync(int id)
    {
        var _dbContact = await GetContactByIdAsync(id);

        if (_dbContact is null)
        {
            throw new NullReferenceException();
        }

        if (_dbContact.EndedAt == DateTime.MinValue)
        {
            _dbContact.EndedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return _dbContact;
        }

        throw new InvalidOperationException("Status já alterado");
    }

    public async Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take)
    {
        var _dbContacts = await GetAllContactsAsync(skip, take);

        if (_dbContacts.Count < 0)
        {
            throw new NullReferenceException();
        }

        _dbContext.Contacts.RemoveRange(_dbContacts);
        await _dbContext.SaveChangesAsync();

        return _dbContacts;
    }

    public async Task<ContactModel> DeleteContactByIdAsync(int id)
    {
        var _dbContact = await GetContactByIdAsync(id);

        if (_dbContact == null)
        {
            throw new NullReferenceException();
        }

        _dbContext.Contacts.Remove(_dbContact);
        await _dbContext.SaveChangesAsync();

        return _dbContact;
    }
}
