using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;
using kazariobranco_backend.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            var validator = new LoginValidator();
            var validation = validator.Validate(request);
            if (validation.IsValid)
            {
                var passwordHasher = new PasswordHasher<LoginRequest>();

                var dbUser = await _dbContext.users
                    .Where(u => u.email == request.email)
                    .FirstOrDefaultAsync();

                if (dbUser == null)
                {
                    return new Response(401, $"unauthorized");
                }

                var isValidHash = passwordHasher.VerifyHashedPassword(request, dbUser.password, request.password);

                switch (isValidHash)
                {
                    case PasswordVerificationResult.Failed:
                        return new Response(401, $"unauthorized");
                    case PasswordVerificationResult.Success:
                        return new Response(200, $"sucess");
                    case PasswordVerificationResult.SuccessRehashNeeded:
                        break;
                }
            }
            return new Response(406, validation.ToString());
        }
        catch (Exception e)
        {
            return new Response(400, e.ToString());
        }
    }

    public async Task<Response> register([FromBody] RegisterRequest request)
    {
        try
        {
            var validator = new RegisterValidate();
            var validation = validator.Validate(request);

            if (validation.IsValid)
            {
                var passwordHasher = new PasswordHasher<RegisterRequest>();
                request.password = passwordHasher.HashPassword(request, request.password);
                request.cpf = passwordHasher.HashPassword(request, request.cpf);

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

                if (query.IsKeySet && isSaved > 0)
                {
                    return new Response(200, "sucess");
                }
            }
            return new Response(406, validation.ToString());
        }
        catch (Exception e)
        {
            return new Response(400, e.ToString());
        }
    }
}
