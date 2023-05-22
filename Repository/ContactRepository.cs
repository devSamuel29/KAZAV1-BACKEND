using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Validator;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    private readonly MyDbContext _dbContext;

    public ContactRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
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
}
