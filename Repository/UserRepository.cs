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
        var dbUsers = await _dbContext.Users.Skip(skip).Take(take).ToListAsync();

        if (dbUsers.Count == 0)
        {
            throw new NullReferenceException("Usuários não encontrados");
        }

        return dbUsers;
    }

    public async Task<UserModel> GetUserByIdAsync(int id)
    {
        var dbUser = await _dbContext.Users.FindAsync(id);

        if (dbUser == null)
        {
            throw new NullReferenceException("Usuário não encontrado");
        }

        return dbUser;
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
                throw new NullReferenceException("Usuário não encontrado");
            }

            var isValidHash = passwordHasher.VerifyHashedPassword(
                request,
                dbUser.Password,
                request.Password
            );

            switch (isValidHash)
            {
                case PasswordVerificationResult.Failed:
                    throw new InvalidOperationException("unauthorized");

                case PasswordVerificationResult.Success:
                    var claims = new List<Claim>() 
                    {
                        new(JwtRegisteredClaimNames.Sub, dbUser.Id.ToString()),
                        new(JwtRegisteredClaimNames.Email, dbUser.Email.ToString()),
                        new("type_user", dbUser.Role)
                    };
                    string token = new JwtSecurityTokenHandler().WriteToken(GetToken(claims));
                    return new Response(200, token);

                case PasswordVerificationResult.SuccessRehashNeeded:
                    break;
            }
        }
        throw new FormatException(validation.ToString());
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
            var date = DateTime.Now;
            
            var newUser = new UserModel()
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Cpf = request.Cpf,
                Phone = request.Phone,
                Email = request.Email,
                Password = request.Password,
                CreatedAt = DateTime.Today,
                UpdatedAt = DateTime.Today
            };

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return new Response(200, "sucess");
        }
        throw new FormatException(validation.ToString());
    }

    public async Task<Response> UpdatePasswordUser(int id, ForgottenPasswordRequest request)
    {
        var dbUser = await GetUserByIdAsync(id);

        var passwordHasher = new PasswordHasher<ForgottenPasswordRequest>();
        request.NewPassword = passwordHasher.HashPassword(request, request.NewPassword);

        dbUser.Password = request.NewPassword;
        dbUser.UpdatedAt = DateTime.Today;
        await _dbContext.SaveChangesAsync();

        return new Response(200, "sucess");
    }

    public async Task<Response> DeleteAllUsersAsync(int skip, int take)
    {
        var dbUsers = await GetAllUsersAsync(skip, take);

        _dbContext.Users.RemoveRange(dbUsers);
        await _dbContext.SaveChangesAsync();
        
        return new Response(200, "sucess");
    }

    public async Task<Response> DeleteUserByIdAsync(int id)
    {
        var dbUser = await GetUserByIdAsync(id);

        _dbContext.Users.Remove(dbUser);
        await _dbContext.SaveChangesAsync();

        return new Response(200, "sucess");
    }
}
