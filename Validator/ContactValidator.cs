using FluentValidation;
using kazariobranco_backend.Request.Contact;

namespace kazariobranco_backend.Validator;

public class ContactValidator : AbstractValidator<ContactRequest>
{
    public ContactValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("Campo nome não anulável!")
            .MinimumLength(5)
            .WithMessage("Escreve pelo menos seu sobrenome!");

        RuleFor(o => o.Email).EmailAddress().WithMessage("Formato de email inválido!");
        
        RuleFor(o => o.Description)
            .NotEmpty()
            .MinimumLength(10)
            .WithMessage("Descreva um pouco melhor o que deseja nos falar")
            .MaximumLength(255)
            .WithMessage("Tamanho máximo de texto atingido!");
    }
}
