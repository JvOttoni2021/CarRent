using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;

namespace CarRent.Tests.Domain;

public class CustomerTests
{
    [Theory(DisplayName = "Deve lançar DomainException na criação se dados vazios")]
    [InlineData("CPF", "", $"{nameof(Customer.Name)} não pode ser vazio.")]
    [InlineData("CPF", null, $"{nameof(Customer.Name)} não pode ser vazio.")]
    [InlineData("", "NOME",$"{nameof(Customer.Cpf)} não pode ser vazio.")]
    [InlineData(null, "NOME",$"{nameof(Customer.Cpf)} não pode ser vazio.")]
    public void DeveLancarDomainExceptionNaCriacaoSeDadosVazios(string? cpf, string? nome, string expectedMessage)
    {
        var exception = Assert.Throws<DomainException>(() => new Customer(
            cpf: cpf,
            nome: nome
        ));
        
        Assert.Contains(expectedMessage, exception.Message);
    }

    [Fact(DisplayName = "Deve criar customer")]
    public void DeveCriarCustomer()
    {
        var customer = Util.CreateValidCustomer();
        
        Assert.NotNull(customer);
    }
}