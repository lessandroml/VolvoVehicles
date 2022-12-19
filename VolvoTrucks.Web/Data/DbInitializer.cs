using System.Linq;
using VolvoTrucks.Models;

namespace VolvoTrucks.Data
{
    public static class DbInitializer
    {
        public static void Initialize(VolvoVehiclesContext context)
        {
            context.Database.EnsureCreated();

            if (context.TruckModels.Any())
            {
                return;
            }

            var models = new TruckModel[]
            {
                new TruckModel { ModelName = "B 10M" },
                new TruckModel { ModelName = "B 10R" },
                new TruckModel { ModelName = "B 12" },
                new TruckModel { ModelName = "B 12B" },
                new TruckModel { ModelName = "B 12M" },
                new TruckModel { ModelName = "B 12R" },
                new TruckModel { ModelName = "B 215RH" },
                new TruckModel { ModelName = "B 240R" },
                new TruckModel { ModelName = "B 270F" },
                new TruckModel { ModelName = "B 290R" },
                new TruckModel { ModelName = "B 340M" },
                new TruckModel { ModelName = "B 340R" },
                new TruckModel { ModelName = "B 360S" },
                new TruckModel { ModelName = "B 380R" },
                new TruckModel { ModelName = "B 420R" },
                new TruckModel { ModelName = "B 450R" },
                new TruckModel { ModelName = "B 58" },
                new TruckModel { ModelName = "B 7R" },
                new TruckModel { ModelName = "B 9" },
                new TruckModel { ModelName = "B 9R" },
                new TruckModel { ModelName = "FH 12" },
                new TruckModel { ModelName = "FH 400" },
                new TruckModel { ModelName = "FH 420" },
                new TruckModel { ModelName = "FH 440" },
                new TruckModel { ModelName = "FH 460" },
                new TruckModel { ModelName = "FH 480" },
                new TruckModel { ModelName = "FH 500" },
                new TruckModel { ModelName = "FH 520" },
                new TruckModel { ModelName = "FH 540" },
                new TruckModel { ModelName = "FH 750" },
                new TruckModel { ModelName = "FM 10" },
                new TruckModel { ModelName = "FM 12" },
                new TruckModel { ModelName = "FM 370" },
                new TruckModel { ModelName = "FM 380" },
                new TruckModel { ModelName = "FM 400" },
                new TruckModel { ModelName = "FM 420" },
                new TruckModel { ModelName = "FM 440" },
                new TruckModel { ModelName = "FM 460" },
                new TruckModel { ModelName = "FM 480" },
                new TruckModel { ModelName = "FM 500" },
                new TruckModel { ModelName = "FM 540" },
                new TruckModel { ModelName = "FMX" },
                new TruckModel { ModelName = "FMX MAX" },
                new TruckModel { ModelName = "NH 12" },
                new TruckModel { ModelName = "NL 10" },
                new TruckModel { ModelName = "NL 12" },
                new TruckModel { ModelName = "VM 17" },
                new TruckModel { ModelName = "VM 210" },
                new TruckModel { ModelName = "VM 220" },
                new TruckModel { ModelName = "VM 23" },
                new TruckModel { ModelName = "VM 260" },
                new TruckModel { ModelName = "VM 270" },
                new TruckModel { ModelName = "VM 310" },
                new TruckModel { ModelName = "VM 330" }
            };

            foreach (TruckModel d in models)
            {
                context.TruckModels.Add(d);
            }
            context.SaveChanges();

            var Trucks = new Truck[]
            {
                new Truck {
                    ModelYear = 2023,
                    ManufacturingYear = 2022,
                    TruckModelID = models.Single( s => s.ModelName == "FH 500").TruckModelID
                },
                new Truck {
                    ModelYear = 2023,
                    ManufacturingYear = 2022,
                    TruckModelID = models.Single( s => s.ModelName == "FM 500").TruckModelID
                },
            };

            foreach (Truck c in Trucks)
            {
                context.Trucks.Add(c);
            }
            context.SaveChanges();
        }
    }
}
