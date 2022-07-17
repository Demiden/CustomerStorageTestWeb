using CustomerStorageTestWeb.Models;
using FluentValidation;

namespace CustomerStorageTestWeb.Validation
{
    public class UpdateCustomerValidation : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidation()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.City).MaximumLength(50);
        }
    }
}