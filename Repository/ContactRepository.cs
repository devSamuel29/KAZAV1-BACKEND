using Azure;
using kazariobranco_backend.Database;
using kazariobranco_backend.Repository.IRepository;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    private readonly MyDbContext _dbContext;
    private readonly IContactRepository _contactRepository;
    public ContactRepository(IContactRepository contactRepository, MyDbContext dbContext)
    {
        _contactRepository = contactRepository;
        _dbContext = dbContext;
    }

    public override async Task<IActionResult> createContactOrder([FromBody] ContactRequest request)
    {
        try
        {
            
        }
        catch (Exception e)
        {
                
        }

    }
}
