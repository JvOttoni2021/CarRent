using CarRent.API.Application.Queries.CarQueries;
using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using CarRent.API.Infraestructure.Persistence.Persistence;
using MediatR;

namespace CarRent.API.Application.Queries.CustomerQueries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _customerRepository.GetCustomerById(request.Id);
        }
    }
}
