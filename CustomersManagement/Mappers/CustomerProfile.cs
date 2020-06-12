using AutoMapper;
using CustomersManagement.DTOs;

namespace CustomersManagement.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Customer.Models.Customer, DataAccess.Databases.Entities.Customer>().ReverseMap();

            CreateMap<Domain.Customer.Models.Customer, CustomerDto>().ReverseMap();
            
            CreateMap<Domain.Customer.Commands.CreateCustomerCommand, CustomerDto>().ReverseMap();

            CreateMap<Domain.Customer.Commands.CreateCustomerCommand, Domain.Customer.Models.Customer>().ReverseMap();
        }
    }
}
