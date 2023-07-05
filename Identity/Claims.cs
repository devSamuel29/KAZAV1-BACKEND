using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace kazariobranco_backend.Identity;

public class Claims
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Role { get; set; }

    public int Exp { get; set; }
}