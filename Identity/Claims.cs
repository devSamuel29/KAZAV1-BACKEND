using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace kazariobranco_backend.Identity;

public class Claims
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public int Exp { get; set; }
}