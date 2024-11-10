using FluentValidation;

namespace NetCore.Application.Order.GetOrder
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>, IValidator
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
