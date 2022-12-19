using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using VolvoTrucks.Data;

namespace VolvoTrucks.Migrations
{
    [DbContext(typeof(VolvoVehiclesContext))]
    partial class VolvoVehiclesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VolvoTrucks.Models.Truck", b =>
                {
                    b.Property<int>("TruckID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TruckModelID");

                    b.Property<int>("ModelYear");

                    b.Property<int>("ManufacturingYear");

                    b.HasKey("TruckID");

                    b.HasIndex("TruckModelID");

                    b.ToTable("Truck");
                });

            modelBuilder.Entity("VolvoTrucks.Models.TruckModel", b =>
                {
                    b.Property<int>("TruckModelID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ModelName")
                        .HasMaxLength(50);

                    b.HasKey("TruckModelID");

                    b.ToTable("TruckModel");
                });

            modelBuilder.Entity("VolvoTrucks.Models.Truck", b =>
                {
                    b.HasOne("VolvoTrucks.Models.TruckModel", "TruckModel")
                        .WithMany("Trucks")
                        .HasForeignKey("TruckModelID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
