using kazariobranco_backend.Request.Auth;

using FluentValidation;

namespace kazariobranco_backend.Validator;

public class CreateChangePwdValidator : AbstractValidator<CreateChangePwdRequest>
{
    public CreateChangePwdValidator()
    {
        RuleFor(p => p.Email).EmailAddress();
    }
}
