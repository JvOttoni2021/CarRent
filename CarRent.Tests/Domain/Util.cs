using CarRent.Domain.Entities;

namespace CarRent.Tests.Domain;

public static class Util
{
    public static Car CreateValidCar()
    {
        return new Car(
            model: "MODEL TESTE",
            maker: "MAKER TESTE",
            year: DateTime.Now.Year,
            dailyPrice: 1.10m
        );
    }

    public static Customer CreateValidCustomer()
    {
        return new Customer(cpf: "56385283003", nome: "NOME TESTE");
    }

    public static Rental CreateValidRental()
    {
        return new Rental(
            customer: CreateValidCustomer(),
            car: CreateValidCar(),
            expectedReturnDate: DateTime.Now.AddDays(10)
        );
    }

    public static PaymentReceipt CreateValidPaymentReceipt()
    {
        return new PaymentReceipt(
            rental: CreateValidRental(),
            rentValue: 100m,
            observation: "OBSERVAÇÃO TESTE"
        );
    }
}