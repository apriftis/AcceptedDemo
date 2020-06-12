using System;
using System.Collections.Generic;
using System.Text;

namespace CustomersManagement.Domain.Customer.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
