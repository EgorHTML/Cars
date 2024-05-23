using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Numerics;
using Cars.Models;

namespace Cars.DataBase
{
    public class DB : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }

        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarFeature>()
                .HasKey(bg => new { bg.CarId, bg.FeatureId });

            modelBuilder.Entity<CarFeature>()
                .HasOne(bg => bg.Car)
                .WithMany(b => b.CarFeatures)
                .HasForeignKey(bg => bg.CarId);

            modelBuilder.Entity<CarFeature>()
                .HasOne(bg => bg.Feature)
                .WithMany(g => g.CarFeatures)
                .HasForeignKey(bg => bg.FeatureId);
        }
    }
}
