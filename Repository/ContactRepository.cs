using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using kazariobranco_backend.Repository.IRepository;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Repository;

public class ContactRepository : IContactRepository
{
    public Task<Response> createContactOrder([FromBody] ContactRequest request)
    {
        throw new NotImplementedException();
    }
}
