using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    private readonly MyDbContext _dbContext;
    public ContactRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> createContactOrder([FromBody] ContactRequest request)
    {
        try
        {
            var id = Guid.NewGuid();
            ContactModel newContact = new ContactModel
            {
                id = id.ToString(),
                name = request.name,
                phone = request.phone,
                email = request.email,
                reason = request.reason,
                description = request.description,
                created_at = DateTime.Today
            };

            var query = await _dbContext.contacts.AddAsync(newContact);
            var isSaved = await _dbContext.SaveChangesAsync();

            var json = new JsonModel
            {
                code = ,
                message = ,
            };

            var response = JsonConvert.SerializeObject(json);
            return response;
        }
        catch (Exception e)
        {
            var json = new JsonModel
            {
                code = 400,
                message = e.ToString()
            };

            var response = JsonConvert.SerializeObject(json);
            return response;
        }
    }
}
