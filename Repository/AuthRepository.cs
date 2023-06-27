using kazariobranco_backend.Models;
using kazariobranco_backend.Request;
using kazariobranco_backend.Database;
using kazariobranco_backend.Validator;
using kazariobranco_backend.Interfaces;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using kazariobranco_backend.Identity;
using System.Security.Claims;

namespace kazariobranco_backend.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IMapper _mapper;

    private readonly IJwtService _jwtService;

    public AuthRepository(MyDbContext dbContext, IMapper mapper, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<JwtSecurityToken> LoginAsync(LoginRequest request)
    {
        var loginValidator = new LoginValidator();
        var validate = await loginValidator.ValidateAsync(request);

        if (validate.IsValid)
        {
            var dbUser = await _dbContext.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (dbUser is null)
            {
                throw new NullReferenceException("Usuário não encontrado!");
            }

            var isValidHash = BCrypt.Net.BCrypt.Verify(request.Password, dbUser.Password);

            if (isValidHash)
            {
                var claims = new List<Claim>()
                {
                    new(JwtRegisteredClaimNames.Sub, dbUser.Id.ToString()),
                    new(JwtRegisteredClaimNames.Name, $"{dbUser.Firstname} {dbUser.Lastname}"),
                    new(JwtRegisteredClaimNames.Email, dbUser.Email),
                    new(IdentityData.ClaimTitle, dbUser.Role)
                };
                return await _jwtService.GetTokenAsync(claims);
            }
            throw new InvalidDataException("dados incorretos!");
        }
        throw new FormatException(validate.Errors.ToString());
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var registerValidate = new RegisterValidate();
        var validate = await registerValidate.ValidateAsync(request);

        if (validate.IsValid)
        {
            await _dbContext.AddAsync(_mapper.Map<UserModel>(request));
            await _dbContext.SaveChangesAsync();
            return;
        }

        throw new InvalidDataException(validate.Errors.ToString());
    }
}
