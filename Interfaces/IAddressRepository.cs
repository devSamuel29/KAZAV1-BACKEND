using kazariobranco_backend.Models;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IAddressRepository 
{ 
    Task<IList<AddressModel>> GetMyAddresses(int id);

    Task AddAddressAsync(AddressRequest Request, UserModel user);
}
