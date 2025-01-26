using CarRent.Domain.Exceptions;

namespace CarRent.Domain.Entities
{
    public class Car
    {
        public int Id { get; }
        public string? Model { get; private set; }
        public string? Maker { get; private set; }
        public int? Year { get; private set; }
        public decimal DailyPrice { get; private set; }
        public bool Available { get; private set; } = true;

        protected Car() { }

        public Car(string? model, string? maker, int? year, decimal dailyPrice)
        {
            Model = model;
            Maker = maker;
            Year = year;
            DailyPrice = dailyPrice;

            IsValid();
        }

        public void ChangeAvailability(bool available)
        {
            Available = available;
        }

        public void Update(string? maker, string? model)
        {
            if (string.IsNullOrEmpty(maker))
                throw new ArgumentNullException(nameof(maker), $"{nameof(maker)} não pode ser vazio");

            if (string.IsNullOrEmpty(model))
                throw new ArgumentNullException(nameof(model), $"{nameof(model)} não pode ser vazio");

            Maker = maker;
            Model = model;
        }

        private void IsValid()
        {
            if (string.IsNullOrEmpty(Model))
                throw new DomainException($"{nameof(Model)} não pode ser vazio.");

            if (string.IsNullOrEmpty(Maker))
                throw new DomainException($"{nameof(Maker)} não pode ser vazio.");

            if (Year is null)
                throw new DomainException($"{nameof(Year)} não pode ser nulo.");

            if (Year > DateTime.Now.Year)
                throw new DomainException($"{nameof(Year)} não pode ser futuro.");

            if (DailyPrice == 0)
                throw new DomainException($"{nameof(DailyPrice)} deve ser maior que 0.");
        }
    }
}
