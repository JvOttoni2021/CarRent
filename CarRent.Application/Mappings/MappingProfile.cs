using AutoMapper;
using CarRent.API.Dtos;
using CarRent.Application.Dtos;
using CarRent.Domain.Entities;

namespace CarRent.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Car, CarDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Rental, RentalDto>();
            CreateMap<PaymentReceipt, PaymentReceiptDto>();
        }
    }
}
