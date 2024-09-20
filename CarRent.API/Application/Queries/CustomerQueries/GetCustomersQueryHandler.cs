using CarRent.API.Domain.Entity;
using CarRent.API.Domain.Interfaces;
using MediatR;

namespace CarRent.API.Application.Queries.CustomerQueries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>?>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>?> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return _customerRepository.GetCustomers();
        }
    }
}
