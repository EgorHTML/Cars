using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models
{
    public class CarFeature
{
    public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car? Car { get; set; }

    public int FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature? Feature { get; set; }
}
}
