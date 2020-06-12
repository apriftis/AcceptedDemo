using CustomersManagement.Infrastructure.Diagnostics;
using MediatR;
using System;

namespace CustomersManagement.Domain.Customer.Commands
{
    public class CreateCustomerCommand : IRequest<IResult<Models.Customer>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
