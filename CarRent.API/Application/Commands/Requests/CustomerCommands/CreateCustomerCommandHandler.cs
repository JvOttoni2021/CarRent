using CarRent.API.Domain.Entity;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.CustomerCommands
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
