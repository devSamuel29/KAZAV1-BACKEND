using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IGuestRepository 
{
    Task<JwtSecurityToken> LoginAsync(LoginRequest request);

    Task RegisterAsync(RegisterRequest request);

    Task ContactAsync(ContactRequest request);

    Task AddCartProductsAsync();

    Task ClearCartAsync();
    
    Task DeleteCartProductsAsync();
}
