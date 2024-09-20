using CarRent.Domain.Entities;

namespace CarRent.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        public bool CpfExists(string cpf);
        public Customer? GetCustomerById(int id);
        public Task<Customer> CreateCustomer(string name, string cpf);
    }
}
