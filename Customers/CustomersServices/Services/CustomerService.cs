using Bogus;
using CustomersServices.Model;
using System.Collections.Generic;

namespace CustomersServices.Services
{
    public class CustomerService : ICustomerService
    {
        private List<Customer> _customers;

        public CustomerService() => GenerateFakeData();

        private void GenerateFakeData()
        {
            _customers = new Faker<Customer>()
                .RuleFor(x => x.Name, (f, u) => f.Name.FirstName())
                .RuleFor(x => x.Surname, (f, u) => f.Name.LastName())
                .RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.Name, u.Surname, "contoso.com"))
                .Generate(20);
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }
    }
}