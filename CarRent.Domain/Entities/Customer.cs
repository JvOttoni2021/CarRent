using CarRent.Domain.Exceptions;

namespace CarRent.Domain.Entities
{
    public class Customer
    {
        public int Id { get; }
        public string? Name { get; init; }
        public string? Cpf { get; init; }

        protected Customer() { }

        public Customer(string? cpf, string? nome) {
            Name = nome;
            Cpf = cpf;

            IsValid();
        }

        private void IsValid()
        {
            if (string.IsNullOrEmpty(Name))
                throw new DomainException($"{nameof(Name)} não pode ser vazio.");

            if (string.IsNullOrEmpty(Cpf))
                throw new DomainException($"{nameof(Cpf)} não pode ser vazio.");
        }
    }
}
