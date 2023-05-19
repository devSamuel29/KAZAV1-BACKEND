using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(p => p.email).EmailAddress().WithMessage("Formato de email inválido");

        RuleFor(p => p.password)
            .MinimumLength(8)
            .WithMessage("Senha muito curta")
            .MaximumLength(16)
            .WithMessage("Senha muito grande")
            .Matches(@"\d")
            .WithMessage("A senha deve conter pelo menos um número")
            .Matches(@"[A-Z]")
            .WithMessage("A senha deve conter uma letra maiúscula");
    }
}
