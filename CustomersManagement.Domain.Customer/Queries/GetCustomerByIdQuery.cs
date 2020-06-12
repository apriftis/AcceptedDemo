using CustomersManagement.Infrastructure.Diagnostics;
using MediatR;
using System;

namespace CustomersManagement.Domain.Customer.Queries
{
    public class GetCustomerByIdQuery : IRequest<IResult<Models.Customer>>
    {
        public Guid CustomerId { get; set; }
        public GetCustomerByIdQuery(Guid customerId)
        {
            this.CustomerId = customerId;
        }
    }
}
