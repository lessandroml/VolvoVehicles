using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using VolvoTrucks.Data;
using VolvoTrucks.Models;

namespace VolvoTrucks.Test
{
    public class TestCaseTruck
    {
        private IConfigurationRoot _configuration;

        private DbContextOptions<VolvoVehiclesContext> _options;

        private int entropy = 10 + new Random().Next(500);

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            var conn = _configuration.GetConnectionString("DefaultConnection");

            _options = new DbContextOptionsBuilder<VolvoVehiclesContext>()
                .UseSqlServer(conn)
                .Options;
        }

        [Test]
        public async Task TestInsert()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();
                
                var truck_model = await context.TruckModels
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync(m => m.TruckModelID == 1);

                Assert.IsNotNull(truck_model);

                var new_truck = new Truck { ModelYear = DateTime.Now.Year + entropy, ManufacturingYear = DateTime.Now.Year, TruckModelID = truck_model.TruckModelID };
                
                context.Add(new_truck);
                var returnId = await context.SaveChangesAsync();

                Assert.IsNotNull(returnId);
            }
        }
        [Test]
        public async Task TestInsertedGet()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var get_truck = await context.Trucks
                    .Include(c => c.TruckModel)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelYear == DateTime.Now.Year + entropy && m.ManufacturingYear == DateTime.Now.Year); // && m.TruckModelID == 1);

                Assert.IsNotNull(get_truck);

                Assert.That(get_truck.ModelYear, Is.EqualTo(DateTime.Now.Year + entropy));
                Assert.That(get_truck.ManufacturingYear, Is.EqualTo(DateTime.Now.Year));
            }
        }
        [Test]
        public async Task TestUpdate()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var upd_truck = await context.Trucks
                    .Include(c => c.TruckModel)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelYear == DateTime.Now.Year + entropy && m.ManufacturingYear == DateTime.Now.Year); // && m.TruckModelID == 1);

                Assert.IsNotNull(upd_truck);

                var upd_truck_model = await context.TruckModels
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync(m => m.TruckModelID == 2);

                Assert.IsNotNull(upd_truck);

                upd_truck.ModelYear = upd_truck.ModelYear + entropy;
                upd_truck.TruckModelID = upd_truck_model.TruckModelID;
                context.Update(upd_truck);
                var returnOldId = await context.SaveChangesAsync();

                Assert.IsNotNull(returnOldId);
            }
        }
        [Test]
        public async Task TestUpdatedGet()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var get_upd_truck = await context.Trucks
                    .Include(c => c.TruckModel)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelYear == DateTime.Now.Year + (entropy * 2) && m.ManufacturingYear == DateTime.Now.Year);// && m.TruckModelID == 2);

                Assert.IsNotNull(get_upd_truck);

                Assert.That(get_upd_truck.ModelYear, Is.EqualTo(DateTime.Now.Year + (entropy * 2)));
                Assert.That(get_upd_truck.ManufacturingYear, Is.EqualTo(DateTime.Now.Year));
            }
        }
        [Test]
        public async Task TestUpdatedRemoved()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var to_delete_truck = await context.Trucks
                    .Include(c => c.TruckModel)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelYear == DateTime.Now.Year + (entropy * 2) && m.ManufacturingYear == DateTime.Now.Year);// && m.TruckModelID == 2);

                Assert.IsNotNull(to_delete_truck);

                context.Remove(to_delete_truck);
                var returnDelId = await context.SaveChangesAsync();

                Assert.IsNotNull(returnDelId);

            }
        }
        [Test]
        public async Task TestVerifyDeleted()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var get_deleted_truck = await context.Trucks
                    .Include(c => c.TruckModel)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelYear == DateTime.Now.Year + (entropy * 2) && m.ManufacturingYear == DateTime.Now.Year);// && m.TruckModelID == 2);

                Assert.IsNull(get_deleted_truck);
            }
        }
    }
}