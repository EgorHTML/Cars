namespace Cars.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
            
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }

        public int BodyTypeId { get; set; }
        public BodyType? BodyType { get; set; }

        public ICollection<CarFeature>? CarFeatures { get; set; }
    }
}
