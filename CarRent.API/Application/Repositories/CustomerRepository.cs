using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using System.Data.Entity;

namespace CarRent.API.Application.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CarRentContext _context;
        public CustomerRepository(CarRentContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToArray();
        }

        public bool CpfExists(string cpf)
        {
            return _context.Customers.Any(c => c.Cpf == cpf);
        }

        public Customer? GetCustomerById(int id) => _context.Customers.Where(c => c.Id == id).FirstOrDefault();
    }
}
