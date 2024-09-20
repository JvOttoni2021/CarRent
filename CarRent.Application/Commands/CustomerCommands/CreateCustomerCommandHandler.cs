using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Commands.CustomerCommands
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
