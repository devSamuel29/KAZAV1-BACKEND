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

    public async Task CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validate = await validator.ValidateAsync(request);

        if (validate.IsValid)
        {
            var contactToAdd = new ContactModel
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Reason = request.Reason,
                Description = request.Description
            };

            await _dbContext.Contacts.AddAsync(contactToAdd);
        }

        throw new FormatException(validate.Errors.ToString());
    }
}
