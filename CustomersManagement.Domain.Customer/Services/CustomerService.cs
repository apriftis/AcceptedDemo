using CustomersManagement.Domain.Customer.Repositories.Interfaces;
using CustomersManagement.Domain.Customer.Services.Interfaces;
using CustomersManagement.Infrastructure.Constants;
using CustomersManagement.Infrastructure.Diagnostics;
using System;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IResult<Models.Customer>> GetByIdAsync(Guid id)
        {
            try
            {
                var customer = await customerRepository.GetByIdAsync(id);
                if (customer is null)
                    return Result<Models.Customer>.CreateFailed(ResultCode.NotFound, $"Could not find customer with id {id}");

                return Result<Models.Customer>.CreateSuccessful(customer);
            }
            catch (Exception e)
            {
                //TODO: Log
                return Result<Models.Customer>.CreateFailed(ResultCode.InternalServerError, $"Failed to get customer with id {id} with error: {e.Message}");
            }
        }

        public async Task<IResult<Models.Customer>> CreateAsync(Models.Customer customer)
        {
            try
            {
                var createdCustomer = customerRepository.Create(customer);
                await customerRepository.CommitAsync();

                return Result<Models.Customer>.CreateSuccessful(createdCustomer);
            }
            catch (Exception e)
            {
                //TODO: log
                return Result<Models.Customer>.CreateFailed(ResultCode.InternalServerError, $"Failed to create customer, with error: {e.Message}");
            }
        }
    }
}
