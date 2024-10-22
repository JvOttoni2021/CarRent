﻿using CarRent.Domain.Entities;
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

        public bool CpfExists(string cpf)
        {
            return _context.Customers.Any(c => c.Cpf == cpf);
        }

        public Customer? GetCustomerById(int id) => _context.Customers.Where(c => c.Id == id).FirstOrDefault();

        public async Task<Customer> CreateCustomer(string name, string cpf)
        {
            var newCustomer = new Customer
            {
                Name = name,
                Cpf = cpf
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return newCustomer;
        }
    }
}
