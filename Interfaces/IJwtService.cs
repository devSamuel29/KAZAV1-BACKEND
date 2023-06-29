using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Identity;

namespace kazariobranco_backend.Interfaces;

public interface IJwtService
{
    Task<JwtSecurityToken> GetTokenAsync(List<Claim> authClaim);

    Task<bool> ReadTokenAsync(string token);

    Task<Claims> GetClaims(string token);
}