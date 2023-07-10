using kazariobranco_backend.Models;
using kazariobranco_backend.Database;
using kazariobranco_backend.Identity;
using kazariobranco_backend.Validator;
using kazariobranco_backend.Interfaces;
using kazariobranco_backend.Request.Auth;

using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using AutoMapper;

namespace kazariobranco_backend.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IMapper _mapper;

    private readonly IJwtService _jwtService;

    private readonly IEmailService _emailService;

    public AuthRepository(
        MyDbContext dbContext,
        IMapper mapper,
        IJwtService jwtService,
        IEmailService emailService
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    public async Task<JwtSecurityToken> LoginAsync(LoginRequest request)
    {
        var loginValidator = new LoginValidator();
        var validate = await loginValidator.ValidateAsync(request);

        if (validate.IsValid)
        {
            var dbUser = await _dbContext.Users
                .Where(u => u.Email == request.Email)
                .FirstAsync();

            var isValidHash = BCrypt.Net.BCrypt.Verify(request.Password, dbUser.Password);

            if (isValidHash)
            {
                var claims = new List<Claim>()
                {
                    new(JwtRegisteredClaimNames.Sub, dbUser.Id.ToString()),
                    new(
                        JwtRegisteredClaimNames.Name,
                        $"{dbUser.Firstname} {dbUser.Lastname}"
                    ),
                    new(JwtRegisteredClaimNames.Email, dbUser.Email),
                    new(IdentityData.ClaimTitle, dbUser.Role)
                };
                await _emailService.SendEmail(
                    request.Email,
                    "LOGIN KAZARIOBRANCO",
                    $"Olá {dbUser.Firstname} {dbUser.Lastname}, você acaba de fazer login em www.site.com as {DateTime.Now}. Se não reconhecer esse login, por favor altere sua senha."
                );
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
            await _emailService.SendEmail(
                request.Email,
                "CADASTRO KAZARIOBRANCO",
                $"Olá {request.Firstname} {request.Lastname}, você acaba de criar uma conta em www.site.com as {DateTime.Now}."
            );
            return;
        }

        throw new InvalidDataException(validate.Errors.ToString());
    }

    public async Task CreateChangePasswordAsync(string email)
    {
        await _dbContext.Users.FirstAsync(p => p.Email == email);

        int code = new Random().Next(100000, 999999);

        await _emailService.SendEmail(
            email,
            "PEDIDO DE MUDANÇA DE SENHA",
            $"aqui está o codigo para mudança de senha: {code}"
        );

        await _dbContext.ChangePassword.AddAsync(
            new ChangePasswordModel() { Email = email, Code = code }
        );
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(ForgottenPasswordRequest request)
    {
        var dbChangePwd = await _dbContext.ChangePassword.FirstAsync(
            p => p.Email == request.Email && p.Code == request.Code
        );

        if (dbChangePwd.IsValid >= DateTime.Now && !dbChangePwd.IsFinished)
        {
            var dbUser = await _dbContext.Users.FirstAsync(p => p.Email == request.Email);
            dbUser.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            dbChangePwd.IsFinished = true;
            await _dbContext.SaveChangesAsync();

            await _emailService.SendEmail(
                request.Email,
                "SENHA ALTERADA - KAZARIOBRANCO",
                $"Sua senha acaba de ser alterada!"
            );
            return;
        }

        throw new Exception(
            "O código enviado não é mais válido ou a senha já foi alterada!"
        );
    }
}
