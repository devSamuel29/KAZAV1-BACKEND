using FluentValidation;
using kazariobranco_backend.Request;

namespace kazariobranco_backend.Validator;

public class ContactValidator : AbstractValidator<ContactRequest>
{
    public ContactValidator()
    {
        
    }
}
