using AutoMapper;
using CustomersManagement.Domain.Customer.Commands;
using CustomersManagement.Domain.Customer.Services.Interfaces;
using CustomersManagement.Infrastructure.Diagnostics;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Handlers.CommandHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, IResult<Models.Customer>>
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CreateCustomerHandler(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        public async Task<IResult<Models.Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
           return await customerService.CreateAsync(mapper.Map<Models.Customer>(request));
        }
    }
}
