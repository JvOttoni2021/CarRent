using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;
using CarRent.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarRent.Application.Commands.CustomerCommands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Customer?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Requisição recebida - Criar cliente.");
                Customer newCustomer = new Customer(request.Cpf, request.Name);
                await _customerRepository.CreateCustomer(newCustomer);

                _logger.LogInformation("Cliente {Id} criado.", newCustomer.Id);
                return newCustomer;
            }
            catch (DomainException ex) {
                _logger.LogError(ex, "Ocorreu um erro ao criar o Cliente, parâmetros inválidos");
            }

            return null;
        }
    }
}
