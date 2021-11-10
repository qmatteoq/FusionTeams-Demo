using CustomersServices.Model;
using System.Collections.Generic;

namespace CustomersServices.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
    }
}