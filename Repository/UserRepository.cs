using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using Newtonsoft.Json;
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

    private string json(int code, string message)
    {
        var json = new JsonModel()
        {
            code = code,
            message = message
        };

        var response = JsonConvert.SerializeObject(json);
        return response;
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

    // public async Task<string> authenticate([FromBody] LoginRequest request)
    // {
    //     // try
    //     // {
    //     //     var response = new Response();
    //     //     var passwordHasher = new PasswordHasher<UserRequest>();
    //     //     request.password = passwordHasher.HashPassword(request, request.password);

    //     //     var dbUser = _dbContext.users
    //     //         .Where(u => u.email == request.email && u.password == request.password)
    //     //         .FirstOrDefault();

    //     //     if (dbUser == null) return null;



    //     // }
    //     // catch (Exception e)
    //     // {
    //     //     return(e.StackTrace.ToString());
    //     // }
    //     return "OI";
    // }

    public async Task<string> register([FromBody] RegisterRequest request)
    {
        try
        {
            var newUser = new UserModel()
            {
                firstname = request.firstname,
                lastname = request.lastname,
                cpf = request.cpf,
                phone = request.phone,
                email = request.email,
                password = request.password,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            var query = await _dbContext.AddAsync(newUser);
            var isSaved = await _dbContext.SaveChangesAsync();

            var response = json(200, "sucess");

            if (!(query.IsKeySet && isSaved > 0))
            {
                response = json(403, "not autorized");
                return response;
            }

            return response;
        }
        catch (Exception e)
        {
            var response = json(400, e.ToString());
            return response;
        }
    }
}
