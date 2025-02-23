using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;

namespace CarRent.Tests.Domain;

public class RentalTests
{
    [Theory(DisplayName = "Deve lançar exception na criação se dados inválidos")]
    [InlineData(true, false, true, $"{nameof(Rental.Customer)} não pode ser nulo")]
    [InlineData(false, true, true, $"{nameof(Rental.RentedCar)} não pode ser nulo")]
    [InlineData(true, true, false, $"{nameof(Rental.ExpectedReturnDate)} não pode estar no passado.")]
    public void DeveLancarExceptionNaCriacaoSeDadosInvalidos(bool validCar, bool validCustomer, bool validDate, string expectedMessage)
    {
        var car = validCar ? Util.CreateValidCar() : null;
        var customer = validCustomer ? Util.CreateValidCustomer() : null;
        var expectedReturnDate = validDate ? DateTime.UtcNow.AddDays(10) : DateTime.UtcNow.AddDays(-10);

        var exception = Assert.Throws<DomainException>(() => new Rental(car, customer, expectedReturnDate));

        Assert.Contains(expectedMessage, exception.Message);
    }

    [Fact(DisplayName = "Deve criar rental")]
    public void DeveCriarRental()
    {
        var exception = Record.Exception(() => Util.CreateValidRental());
        
        Assert.Null(exception);
    }

    [Fact(DisplayName = "ReturnCar deve alterar estado da rental")]
    public void ReturnCarDeveAlterarEstado()
    {
        var rental = Util.CreateValidRental();
        
        
        Assert.False(rental.CarReturned);
        Assert.Null(rental.ReturnDate);
        
        rental.ReturnCar();
        
        Assert.True(rental.CarReturned);
        Assert.NotNull(rental.ReturnDate);
    }

    [Fact(DisplayName = "ReturnCar deve lançar DomainException ao tentar retornar carro em rental já finalizada")]
    public void ReturnCarDeveLancarExceptionSeReexecutado()
    {
        var rental = Util.CreateValidRental();
        
        rental.ReturnCar();
        
        Assert.Throws<DomainException>(() => rental.ReturnCar());
    }
}