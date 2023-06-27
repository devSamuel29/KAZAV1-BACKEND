using System.IdentityModel.Tokens.Jwt;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Interfaces;

public interface IAuthRepository
{
    Task<JwtSecurityToken> LoginAsync(LoginRequest request);

    Task RegisterAsync(RegisterRequest request);
}
