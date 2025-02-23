using CarRent.Domain.Entities;
using CarRent.Domain.Exceptions;

namespace CarRent.Tests.Domain;

public class PaymentReceiptTest
{
    [Theory(DisplayName = "Deve lançar DomainException ao criar objeto com dados inválidos")]
    [InlineData(false, 1.0, "OBSERVAÇÃO", $"{nameof(PaymentReceipt.Rental)} não pode ser nulo.")]
    [InlineData(true, 0, "OBSERVAÇÃO", $"{nameof(PaymentReceipt.RentValue)} deve possuir um valor positivo.")]
    [InlineData(true, 1.0, "", $"{nameof(PaymentReceipt.Observation)} não pode ser vazio.")]
    [InlineData(true, 1.0, null, $"{nameof(PaymentReceipt.Observation)} não pode ser vazio.")]
    public void DeveLancarDomainExceptionAoCriarComDadosInvalidos(bool validRental, decimal rentValue, string? observation, string expectedMessage)
    {
        Rental? rental = validRental ? Util.CreateValidRental() : null;

        var exception = Assert.Throws<DomainException>(() => new PaymentReceipt(rental, rentValue, observation));

        Assert.Contains(expectedMessage, exception.Message);
    }

    [Fact(DisplayName = "Deve criar PaymentReceipt")]
    public void DeveCriarPaymentReceipt()
    {
        PaymentReceipt paymentReceipt = Util.CreateValidPaymentReceipt();
        
        Assert.NotNull(paymentReceipt);
    }
}