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
        return await _dbContext.Contacts.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<ContactModel> GetContactByIdAsync(int id)
    {
        return await _dbContext.Contacts.FindAsync(id);
    }

    // public async Task<ContactModel> GetContactByNameAsync(string name)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByNameAsync(string name)
    {
        return await _dbContext.Contacts.Where(u => u.Name == name).ToListAsync();
    }

    // public async Task<ContactModel> GetContactByPhoneAsync(string phone)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByPhoneAsync(string phone)
    {
        return await _dbContext.Contacts.Where(u => u.Phone == phone).ToListAsync();
    }

    // public async Task<ContactModel> GetContactByEmailAsync(string email)
    // {

    // }

    public async Task<List<ContactModel>> GetContactsByEmailAsync(string email)
    {
        return await _dbContext.Contacts.Where(u => u.Email == email).ToListAsync();
    }

    public async Task<Response> CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validation = validator.Validate(request);

        if (validation.IsValid)
        {
            ContactModel newContact = new ContactModel
            {
                Name = request.name,
                Phone = request.phone,
                Email = request.email,
                Reason = request.reason,
                Description = request.description,
                CreatedAt = DateTime.Today
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

    public Task<Response> UpdateStatusContactAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take)
    {
        var dbContacts = await GetAllContactsAsync(skip, take);

        _dbContext.Contacts.RemoveRange(dbContacts);
        await _dbContext.SaveChangesAsync();

        return dbContacts;
    }

    public async Task<ContactModel> DeleteContactById(int id)
    {
        var dbContact = await GetContactByIdAsync(id);

        _dbContext.Contacts.Remove(dbContact);
        await _dbContext.SaveChangesAsync();

        return dbContact;
    }
}
