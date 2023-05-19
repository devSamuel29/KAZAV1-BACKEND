using System.Text.RegularExpressions;
using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class RegisterValidate : AbstractValidator<RegisterRequest>
{
    public RegisterValidate()
    {
        RuleFor(p => p.firstname)
            .MinimumLength(3)
            .WithMessage("Nome muito curto")
            .MaximumLength(20)
            .WithMessage("Nome muito grande");

        RuleFor(p => p.lastname)
            .MinimumLength(3)
            .WithMessage("Sobrenome muito curto")
            .MaximumLength(20)
            .WithMessage("Sobrenome muito grande");

        RuleFor(p => p.cpf).NotEmpty().Must(cpfValidate);

        RuleFor(p => p.phone).NotEmpty();

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

    public bool cpfValidate(string cpf)
    {
        return true;
    }
}
