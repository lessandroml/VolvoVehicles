using VolvoTrucks.Models;
using Microsoft.EntityFrameworkCore;

namespace VolvoTrucks.Data
{
    public class VolvoVehiclesContext : DbContext
    {
        public VolvoVehiclesContext(DbContextOptions<VolvoVehiclesContext> options) : base(options)
        {
        }

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckModel> TruckModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Truck>().ToTable("Truck");
            modelBuilder.Entity<TruckModel>().ToTable("TruckModel");
        }
    }
}
