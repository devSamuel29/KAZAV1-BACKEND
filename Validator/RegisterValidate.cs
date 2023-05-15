using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class RegisterValidate : AbstractValidator<RegisterRequest>
{
    public RegisterValidate()
    {
        RuleFor(p => p.firstname)
            .NotEmpty()
            .MaximumLength(20)
            .MinimumLength(3);

        RuleFor(p => p.lastname)
            .NotEmpty()
            .MaximumLength(20)
            .MinimumLength(3);

        RuleFor(p => p.cpf)
            .NotEmpty()
            .Must(cpfValidate);

        RuleFor(p => p.phone)
            .NotEmpty();

        RuleFor(p => p.email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(p => p.password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16);
    }

    public bool cpfValidate(string cpf)
    {
        return true;
    }
}
