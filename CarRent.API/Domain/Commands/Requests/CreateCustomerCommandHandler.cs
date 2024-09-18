﻿using CarRent.API.Application.Persistence;
using CarRent.API.Domain.Entity;
using MediatR;

namespace CarRent.API.Domain.Commands.Requests
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly CarRentContext context;

        public CreateCustomerCommandHandler(CarRentContext context)
        {
            this.context = context;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Cpf = request.Cpf
            };

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return customer;
        }
    }
}