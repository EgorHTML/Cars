﻿namespace Cars.Models
{
    public class Feature
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<CarFeature>? CarFeatures { get; set; }
}
}

