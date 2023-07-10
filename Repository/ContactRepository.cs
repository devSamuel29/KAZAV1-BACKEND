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

    private readonly IEmailService _emailService;

    private readonly IMapper _mapper;

    public ContactRepository(
        MyDbContext dbContext,
        IMapper mapper,
        IEmailService emailService
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validate = await validator.ValidateAsync(request);

        if (validate.IsValid)
        {
            await _dbContext.Contacts.AddAsync(_mapper.Map<ContactModel>(request));
            await _dbContext.SaveChangesAsync();
            await _emailService.SendEmail(
                request.Email,
                $"{request.Reason} - KAZARIOBRANCO",
                $"Olá, {request.Name}! Acabamos de receber uma solicitação de contato: {request.Description}. Responderemos o quanto antes. Fique atento ao email e número de telefone fornecidos para contato."
            );
            return;
        }

        throw new InvalidDataException(validate.ToString());
    }
}
