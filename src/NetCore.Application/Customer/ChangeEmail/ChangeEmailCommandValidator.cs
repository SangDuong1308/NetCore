﻿using FluentValidation;

namespace NetCore.Application.Customer.ChangeEmail
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>, IValidator
    {
        public ChangeEmailCommandValidator()
        {
            RuleFor(x => x.OldEmail).NotEmpty().MaximumLength(400);
            RuleFor(x => x.NewEmail).NotEmpty().MaximumLength(400);
        }
    }
}
