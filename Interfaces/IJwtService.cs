using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace kazariobranco_backend.Interfaces;

public interface IJwtService
{
    Task<JwtSecurityToken> GetTokenAsync(List<Claim> authClaim);
}