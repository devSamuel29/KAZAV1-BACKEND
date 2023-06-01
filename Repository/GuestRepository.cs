using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Validator;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Models;

namespace kazariobranco_backend.Repository;

public class GuestRepository : IGuestRepository
{
    private readonly IConfiguration _config;
    private readonly MyDbContext _dbContext;

    public GuestRepository(IConfiguration config, MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _config = config;
    }

    public async Task CreateContactAsync(ContactRequest request)
    {
        var validator = new ContactValidator();
        var validate = await validator.ValidateAsync(request);

        if (validate.IsValid)
        {
            var contactToAdd = new ContactModel
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Reason = request.Reason,
                Description = request.Description
            };

            await _dbContext.Contacts.AddAsync(contactToAdd);
        }

        throw new FormatException(validate.Errors.ToString());
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

    public async Task<JwtSecurityToken> LoginAsync(LoginRequest request)
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
                        new(JwtRegisteredClaimNames.Name, dbUser.Firstname),
                        new(JwtRegisteredClaimNames.Email, dbUser.Email),
                        new(IdentityData.Role, dbUser.Role)
                    };

                    return GetToken(claims);

                case PasswordVerificationResult.SuccessRehashNeeded:
                    break;
            }
        }
        throw new FormatException(validation.ToString());
    }

    public async Task RegisterAsync(RegisterRequest request)
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
        }
        throw new FormatException(validation.ToString());
    }

    public Task ContactAsync(ContactRequest request)
    {
        throw new NotImplementedException();
    }

    public Task AddCartProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task ClearCartAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteCartProductsAsync()
    {
        throw new NotImplementedException();
    }
}
