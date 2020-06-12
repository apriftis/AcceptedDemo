using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomersManagement.DataAccess.Databases;
using CustomersManagement.Domain.Customer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext context;
        private readonly IMapper mapper;

        public CustomerRepository(CustomerDbContext customerDbContext, IMapper mapper)
        {
            this.context = customerDbContext;
            this.mapper = mapper;
        }

        public Task<List<Models.Customer>> GetAsync()
        {
            return context.Customers
                .ProjectTo<Models.Customer>(null)
                .ToListAsync();
        }

        public async Task<Models.Customer> GetByIdAsync(Guid id)
        {
            var customerEntity = await context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customerEntity == null)
                return null;

            return mapper.Map<Models.Customer>(customerEntity);
        }

        public Models.Customer Create(Models.Customer customer)
        {
            var createdEntity = context.Customers.Add(mapper.Map<DataAccess.Databases.Entities.Customer>(customer));

            return mapper.Map<Models.Customer>(createdEntity.Entity);
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
