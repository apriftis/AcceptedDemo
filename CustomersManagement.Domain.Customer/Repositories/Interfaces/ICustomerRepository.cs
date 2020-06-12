using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task CommitAsync();
        Models.Customer Create(Models.Customer customer);
        Task<List<Models.Customer>> GetAsync();
        Task<Models.Customer> GetByIdAsync(Guid id);
    }
}
