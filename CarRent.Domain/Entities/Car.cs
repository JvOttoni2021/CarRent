namespace CarRent.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Maker { get; set; }
        public int? Year { get; set; }
        public decimal DailyPrice { get; set; }
        public bool Available { get; set; } = true;

    }
}
