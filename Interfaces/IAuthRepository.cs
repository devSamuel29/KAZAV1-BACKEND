using System.IdentityModel.Tokens.Jwt;
using kazariobranco_backend.Request.Auth;

namespace kazariobranco_backend.Interfaces;

public interface IAuthRepository
{
    Task<JwtSecurityToken> LoginAsync(LoginRequest request);

    Task RegisterAsync(RegisterRequest request);

    Task CreateChangePasswordAsync(string email);

    Task ReadTest(string email, int code);
    
    Task UpdatePasswordAsync(ForgottenPasswordRequest request);
}
