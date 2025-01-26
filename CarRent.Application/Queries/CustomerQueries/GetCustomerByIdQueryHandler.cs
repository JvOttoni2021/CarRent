using CarRent.Domain.Entities;
using CarRent.Domain.Interfaces;
using MediatR;

namespace CarRent.Application.Queries.CustomerQueries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_customerRepository.GetCustomerById(request.Id));
        }
    }
}
