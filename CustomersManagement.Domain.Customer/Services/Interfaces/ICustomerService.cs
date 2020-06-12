using CustomersManagement.Infrastructure.Diagnostics;
using System;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult<Models.Customer>> CreateAsync(Models.Customer customer);
        Task<IResult<Models.Customer>> GetByIdAsync(Guid id);
    }
}
