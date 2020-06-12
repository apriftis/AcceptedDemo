using CustomersManagement.Domain.Customer.Queries;
using CustomersManagement.Domain.Customer.Services.Interfaces;
using CustomersManagement.Infrastructure.Diagnostics;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Handlers.QueryHandlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, IResult<Models.Customer>>
    {
        private readonly ICustomerService customerService;

        public GetCustomerByIdHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<IResult<Models.Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await customerService.GetByIdAsync(request.CustomerId);
        }
    }
}
