using System.Text;
using System.Security.Claims;
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

    public async Task<Response> authenticate(LoginRequest request)
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
                throw new ArgumentNullException("not found");
            }

            var isValidHash = passwordHasher.VerifyHashedPassword(
                request,
                dbUser.password,
                request.password
            );

            // if (isValidHash == PasswordVerificationResult.Failed) { }
            // else
            // {
            //     // pode ser 1 ou 2 (ambos sucess)
            // }

            switch (isValidHash)
            {
                case PasswordVerificationResult.Failed:
                    throw new InvalidOperationException("unauthorized");
                case PasswordVerificationResult.Success:
                    return new Response(200, "a");
                case PasswordVerificationResult.SuccessRehashNeeded:
                    // colocar um metodo pra verificar se o cpf ou a senha
                    // precisam de um rehash
                    // colocar um if no lugar do switch
                    break;
            }
        }
        throw new FormatException(validation.ToString());
    }

    public async Task<Response> register(RegisterRequest request)
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
        throw new FormatException(validation.ToString());
    }
}
