using CarRent.API.Domain.Entity;

namespace CarRent.API.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        public bool CpfExists(string cpf);
    }
}
