using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;

namespace CarRent.Tests.Domain;

public class CarTests
{
    [Theory(DisplayName = "Deve lançar exception se dado inválido na criação")]
    [InlineData("", "MAKER TEST", 2001, 1.1, $"{nameof(Car.Model)} não pode ser vazio.")]
    [InlineData(null, "MAKER TEST", 2001, 1.1, $"{nameof(Car.Model)} não pode ser vazio.")]
    [InlineData("MODEL TEST", "", 2001, 1.1, $"{nameof(Car.Maker)} não pode ser vazio.")]
    [InlineData("MODEL TEST", null, 2001, 1.1, $"{nameof(Car.Maker)} não pode ser vazio.")]
    [InlineData("MODEL TEST", "MAKER TEST", null, 1.1, $"{nameof(Car.Year)} não pode ser nulo.")]
    [InlineData("MODEL TEST", "MAKER TEST", 9999, 1.1, $"{nameof(Car.Year)} não pode ser futuro.")]
    [InlineData("MODEL TEST", "MAKER TEST", 2001, 0, $"{nameof(Car.DailyPrice)} deve ser maior que 0.")]
    public void DeveLancarExceptionSeDadoInvalidoCriacao(string? model, 
        string? maker, 
        int? year, 
        decimal dailyPrice, 
        string expectedMessage)
    {
        var exception = Assert.Throws<DomainException>(() => new Car(
            model: model,
            maker: maker,
            year: year,
            dailyPrice: dailyPrice
        ));

        Assert.Contains(expectedMessage, exception.Message);
    }

    [Fact(DisplayName = "Deve criar Carro disponível")]
    public void DeveCriarCarroDisponivel()
    {
        var car = Util.CreateValidCar();

        Assert.NotNull(car);
        Assert.True(car.Available);
    }

    [Theory(DisplayName = "Update deve lançar DomainException se valores vazios")]
    [InlineData("", "MAKER")]
    [InlineData("MODEL", "")]
    public void UpdateDeveLancarExceptionSeDadosInvalidos(string model, string maker)
    {
        var car = Util.CreateValidCar();

        Assert.Throws<ArgumentNullException>(() => car.Update(maker, model));
    }

    [Fact(DisplayName = "Update deve atualizar os dados se dados informados não forem vazios")]
    public void UpdateDeveExecutarSeValoresNaoVazios()
    {
        var car = Util.CreateValidCar();

        var exception = Record.Exception(() => car.Update("MAKER TESTE", "MODEL TESTE"));
        
        Assert.Null(exception);
    }

    [Fact(DisplayName = "ChangeAvailability deve alterar disponibilidade do carro")]
    public void ChangeAvailabilityDeveAlterarDisponibilidade()
    {
        var car = Util.CreateValidCar();
        
        Assert.True(car.Available);
        
        car.ChangeAvailability(false);
        
        Assert.False(car.Available);
    }
}