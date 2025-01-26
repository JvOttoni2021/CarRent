using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using CarRent.Infrastructure.DbContext;

namespace CarRent.Infraestructure.Repositories
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

        public Customer? GetCustomerByCpf(string cpf) => _context.Customers.Where(c => c.Cpf == cpf).FirstOrDefault();

        public Customer? GetCustomerById(int id) => _context.Customers.Where(c => c.Id == id).FirstOrDefault();

        public async Task CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
    }
}
