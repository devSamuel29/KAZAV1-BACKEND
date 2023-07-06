using kazariobranco_backend.Models;
using kazariobranco_backend.Database;
using kazariobranco_backend.Validator;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Contact;

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
}
