using System.IdentityModel.Tokens.Jwt;
using kazariobranco_backend.Request.Auth;

namespace kazariobranco_backend.Interfaces;

public interface IAuthRepository
{
    Task<JwtSecurityToken> LoginAsync(LoginRequest request);

    Task<JwtSecurityToken> RegisterAsync(RegisterRequest request);

    Task CreateChangePasswordAsync(CreateChangePwdRequest request);
    
    Task<JwtSecurityToken> UpdatePasswordAsync(ChangePasswordRequest request);
}
