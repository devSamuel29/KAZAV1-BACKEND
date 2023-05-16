using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(p => p.email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(p => p.password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16);
    }
}
