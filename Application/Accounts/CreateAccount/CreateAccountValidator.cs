using FluentValidation;

namespace Application.Accounts.CreateAccount;

public class CreateAccountValidator: AbstractValidator<CreateAccountRequest>
{
    public CreateAccountValidator()
    {
        RuleFor(acc=>acc.UserId).GreaterThan(0).WithMessage("UserId is required.");
        RuleFor(acc=>acc.Name).NotEmpty().WithMessage("Name is required.");
        
    }
}