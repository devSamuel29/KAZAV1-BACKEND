using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using Microsoft.AspNetCore.Mvc;

namespace kazariobranco_backend.Interfaces;

public interface IContactRepository
{
    Task<List<ContactModel>> GetAllContactsAsync(int skip, int take);

    Task<ContactModel> GetContactByIdAsync(int id);
    
    // Task<List<ContactModel>> GetContactsByNameAsync(string name);

    // Task<ContactModel> GetContactByNameAsync(string name);
    
    // Task<ContactModel> GetContactByPhoneAsync(string phone);
    
    // Task<ContactModel> GetContactByEmailAsync(string email);

    Task<Response> CreateContactAsync(ContactRequest request);

    Task<ContactModel> UpdateStatusContactByIdAsync(int id);

    Task<List<ContactModel>> DeleteAllContactsAsync(int skip, int take);

    Task<ContactModel> DeleteContactById(int id);
}
