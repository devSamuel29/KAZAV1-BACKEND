using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    private readonly MyDbContext _dbContext;
    
    public ContactRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> createContactOrder([FromBody] ContactRequest request)
    {
        try
        {
            ContactModel newContact = new ContactModel
            {
                name = request.name,
                phone = request.phone,
                email = request.email,
                reason = request.reason,
                description = request.description,
                created_at = DateTime.Today
            };

            var query = await _dbContext.contacts.AddAsync(newContact);
            var isSaved = await _dbContext.SaveChangesAsync();

            if (query.IsKeySet && isSaved > 0)
            {
                return new Response(200, "sucess");
            }

            return new Response(406, "nao me lembro");
        }

        catch (Exception e)
        {
            return new Response(400, e.ToString());
        }
    }
}
