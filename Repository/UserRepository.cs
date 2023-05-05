using Azure;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Repository.IRepository;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;
    private readonly IConfiguration _config;

    public UserRepository(IConfiguration config, MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _config = config;
    }

    private JwtSecurityToken getToken(List<Claim> authClaim)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaim,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public async Task<Response> authenticate([FromBody] LoginRequest request)
    {
        // try
        // {
        //     var response = new Response();
        //     var passwordHasher = new PasswordHasher<UserRequest>();
        //     request.password = passwordHasher.HashPassword(request, request.password);

        //     var dbUser = _dbContext.users
        //         .Where(u => u.email == request.email && u.password == request.password)
        //         .FirstOrDefault();

        //     if (dbUser == null) return null;



        // }
        // catch (Exception e)
        // {
        //     return(e.StackTrace.ToString());
        // }
    }

    public Task<Response> register([FromBody] RegisterRequest request) { }
}
