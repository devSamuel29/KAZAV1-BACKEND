using FluentValidation;
using kazariobranco_backend.Request.Auth;

namespace kazariobranco_backend.Validator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(p => p.Email).EmailAddress().WithMessage("Formato de email inválido!");
    }
}
