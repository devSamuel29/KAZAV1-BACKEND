using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace kazariobranco_backend.Identity;

public class Claims
{
    public string UserId { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public IList<Claim> GetClaims { get; }

    public Claims()
    {
        GetClaims = new List<Claim>() {
            new(JwtRegisteredClaimNames.Sub, UserId),
            new(JwtRegisteredClaimNames.Name, $"{Firstname} {Lastname}"),
            new(JwtRegisteredClaimNames.Email, Email),
            new(IdentityData.ClaimTitle, Role)
        };
    }
}