namespace CarRent.API.Domain.Entity
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string CarMaker { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; } = true;
    }
}
