using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class ContactValidator : AbstractValidator<ContactRequest>
{
    public ContactValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("Nome vazio")
            .MinimumLength(5)
            .WithMessage("PEQUENO DEMIASI");

        RuleFor(o => o.Email).EmailAddress().WithMessage("FORMATO DE EMAIL");

        RuleFor(o => o.Description)
            .NotEmpty()
            .MinimumLength(10)
            .WithMessage("MUITO PEQUENO")
            .MaximumLength(255)
            .WithMessage("MUITO GRANDE");
    }
}
