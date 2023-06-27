using kazariobranco_backend.Interfaces;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

namespace PROJETO.Domain.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<JwtSecurityToken> GetTokenAsync(List<Claim> authClaim)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaim,
            signingCredentials: new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return await Task.FromResult(token);
    }

    public async Task<string> ReadTokenAsync(string token)
    {
        var tokenHandler = await new JwtSecurityTokenHandler().ValidateTokenAsync(
            token,
            new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"])
                )
            }
        );

        return await Task.FromResult(JsonConvert.SerializeObject(tokenHandler.Claims));
    }

    // //TO-DO
    // public async Task<JwtClaimsResponse> MapClaims(string token)
    // {
    //     var tokenHandler = await ReadTokenAsync(token);
        
    // }
}