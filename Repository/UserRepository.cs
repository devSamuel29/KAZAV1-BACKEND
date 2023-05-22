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

    public async Task<List<UserModel>> GetAllUsersAsync(int skip, int take)
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<Response> Authenticate(LoginRequest request)
    {
        var validator = new LoginValidator();
        var validation = validator.Validate(request);
        if (validation.IsValid)
        {
            var passwordHasher = new PasswordHasher<LoginRequest>();

            var dbUser = await _dbContext.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (dbUser == null)
            {
                throw new ArgumentNullException("not found");
            }

            var isValidHash = passwordHasher.VerifyHashedPassword(
                request,
                dbUser.Password,
                request.Password
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

    private JwtSecurityToken GetToken(List<Claim> authClaim)
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

    public async Task<Response> Register(RegisterRequest request)
    {
        var validator = new RegisterValidate();
        var validation = validator.Validate(request);

        if (validation.IsValid)
        {
            var passwordHasher = new PasswordHasher<RegisterRequest>();
            request.Password = passwordHasher.HashPassword(request, request.Password);
            request.Cpf = passwordHasher.HashPassword(request, request.Cpf);

            var newUser = new UserModel()
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Cpf = request.Cpf,
                Phone = request.Phone,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
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

    public async Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request)
    {
        var dbUser = await GetUserByIdAsync(id);

        if (dbUser == null)
        {
            throw new ArgumentNullException("not found");
        }

        var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        dbUser.Password = request.NewPassword;
        await _dbContext.SaveChangesAsync();

        return new Response(200, "sucess");
    }

    public async Task<List<UserModel>> DeleteAllUsersAsync(int skip, int take)
    {
        var dbUsers = await GetAllUsersAsync(skip, take);

        _dbContext.Users.RemoveRange(dbUsers);
        await _dbContext.SaveChangesAsync();

        return dbUsers;
    }

    public async Task<UserModel> DeleteUserByIdAsync(int id)
    {
        var dbUser = await GetUserByIdAsync(id);

        if (dbUser == null)
        {
            throw new ArgumentNullException("usuário não encontrado");
        }

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();

        return dbUser;
    }
}
