using FluentValidation;

namespace NetCore.Application.Customer.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>, IValidator
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(400);
        }
    }
}
