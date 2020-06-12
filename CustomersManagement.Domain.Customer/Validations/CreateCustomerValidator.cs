using CustomersManagement.Domain.Customer.Commands;
using FluentValidation;

namespace CustomersManagement.Domain.Customer.Validations
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty();
            RuleFor(m => m.LastName).NotEmpty();
        }
    }
}
