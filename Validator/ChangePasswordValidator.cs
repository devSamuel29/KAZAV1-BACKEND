using kazariobranco_backend.Request.Auth;

using FluentValidation;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(p => p.Email).EmailAddress();

        RuleFor(p => p.Code).InclusiveBetween(100000, 999999);
        
        RuleFor(p => p.NewPassword)
            .MinimumLength(8)
            .WithMessage("Senha muito curta!")
            .MaximumLength(16)
            .WithMessage("Senha muito grande!")
            .Matches(@"\d")
            .WithMessage("A senha deve conter pelo menos um número!")
            .Matches(@"[A-Z]")
            .WithMessage("A senha deve conter uma letra maiúscula!");
    }
}
