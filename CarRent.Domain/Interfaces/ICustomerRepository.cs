using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        public Customer? GetCustomerByCpf(string cpf);
        public Customer? GetCustomerById(int id);
        public Task CreateCustomer(Customer customer);
    }
}
