using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;

namespace CarRent.API.Application.Commands.Requests.CustomerCommands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Requisição recebida - Criar cliente.");
            Customer newCustomer = await _customerRepository.CreateCustomer(request.Name, request.Cpf);

            Console.WriteLine($"Cliente {newCustomer.Id} criado.\n" +
                $"Requisição finalizada - Criar cliente.");
            return newCustomer;
        }
    }
}
