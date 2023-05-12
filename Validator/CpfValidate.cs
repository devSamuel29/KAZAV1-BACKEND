using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Atributes;

public class CpfValidate : AbstractValidator<RegisterRequest>
{  
    public CpfValidate()
    {
        RuleFor(p => p.firstname)
            .NotEmpty()
            .WithMessage("")
            .MaximumLength(3);
    }
}
