namespace Cars.Models
{
    public class BodyType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}
