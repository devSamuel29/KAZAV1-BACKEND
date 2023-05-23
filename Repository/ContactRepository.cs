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

    public ContactRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ContactModel>> GetAllContactsAsync(int skip, int take)
    {
        var dbContacts = await _dbContext.Contacts.Skip(skip).Take(take).ToListAsync();

        if (dbContacts.Count == 0)
        {
            throw new NullReferenceException("");
        }

        return dbContacts;
    }

    public async Task<ContactModel> GetContactByIdAsync(int id)
    {
        var dbContact = await _dbContext.Contacts.FindAsync(id);

        if (dbContact == null)
        {
            throw new NullReferenceException("");
        }

        return dbContact;
    }

    // public async Task<ContactModel> GetContactByNameAsync(string name)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByNameAsync(string name)
    {
        var dbContacts = await _dbContext.Contacts.Where(u => u.Name == name).ToListAsync();

        if (dbContacts.Count == 0)
        {
            throw new NullReferenceException("");
        }

        return dbContacts;
    }

    // public async Task<ContactModel> GetContactByPhoneAsync(string phone)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByPhoneAsync(string phone)
    {
        var dbContact = await _dbContext.Contacts.Where(u => u.Phone == phone).ToListAsync();

        if (dbContact == null)
        {
            throw new NullReferenceException("");
        }

        return dbContact;
    }

    // public async Task<ContactModel> GetContactByEmailAsync(string email)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByEmailAsync(string email)
    {
        var dbContact = await _dbContext.Contacts.Where(u => u.Email == email).ToListAsync();

        if (dbContact == null)
        {
            throw new NullReferenceException("");
        }

        return dbContact;
    }

    public async Task<Response> CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validation = validator.Validate(request);

        if (validation.IsValid)
        {
            ContactModel newContact = new ContactModel
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Reason = request.Reason,
                Description = request.Description,
                CreatedAt = DateTime.Today,
            };

            var query = await _dbContext.Contacts.AddAsync(newContact);
            var isSaved = await _dbContext.SaveChangesAsync();

            if (query.IsKeySet && isSaved > 0)
            {
                return new Response(200, "sucess");
            }
        }
        throw new FormatException(validation.ToString());
    }

    public async Task<ContactModel> UpdateStatusContactByIdAsync(int id)
    {
        var dbContact = await GetContactByIdAsync(id);

        if (dbContact.EndedAt == DateTime.MinValue)
        {
            dbContact.EndedAt = DateTime.Today;
            await _dbContext.SaveChangesAsync();

            return dbContact;
        }
        throw new Exception("NAO SEI AINDA");
    }

    public async Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take)
    {
        var dbContacts = await GetAllContactsAsync(skip, take);

        if (dbContacts == null)
        {
            throw new NullReferenceException("");
        }

        _dbContext.Contacts.RemoveRange(dbContacts);
        await _dbContext.SaveChangesAsync();

        return dbContacts;
    }

    public async Task<ContactModel> DeleteContactById(int id)
    {
        var dbContact = await GetContactByIdAsync(id);

        if (dbContact == null)
        {
            throw new NullReferenceException("");
        }

        _dbContext.Contacts.Remove(dbContact);
        await _dbContext.SaveChangesAsync();

        return dbContact;
    }
}
