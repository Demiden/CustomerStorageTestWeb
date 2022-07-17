using CustomerStorageTestWeb.Models;
using FluentValidation;

namespace CustomerStorageTestWeb.Validation
{
    public class AddCustomerValidation : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerValidation()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.City).MaximumLength(50);
        }
    }
}