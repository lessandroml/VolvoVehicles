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
    public class TestCaseTruckModel
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
                
                var new_truck_model = new TruckModel { ModelName = "TEST " + entropy };
                
                context.Add(new_truck_model);
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

                var get_truck_model = await context.TruckModels
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelName == "TEST " + entropy);

                Assert.IsNotNull(get_truck_model);

                Assert.That(get_truck_model.ModelName, Is.EqualTo("TEST " + entropy));
            }
        }
        [Test]
        public async Task TestUpdate()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var upd_truck_model = await context.TruckModels
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelName == "TEST " + entropy);

                Assert.IsNotNull(upd_truck_model);

                upd_truck_model.ModelName = "UTEST " + entropy;

                context.Update(upd_truck_model);
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

                var get_upd_truck = await context.TruckModels
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelName == "UTEST " + entropy);

                Assert.IsNotNull(get_upd_truck);

                Assert.That(get_upd_truck.ModelName, Is.EqualTo("UTEST " + entropy));
            }
        }
        [Test]
        public async Task TestUpdatedRemoved()
        {
            using (var context = new VolvoVehiclesContext(_options))
            {
                context.Database.EnsureCreated();

                var to_delete_truck_model = await context.TruckModels
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelName == "UTEST " + entropy);

                Assert.IsNotNull(to_delete_truck_model);

                context.Remove(to_delete_truck_model);
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

                var get_deleted_truck = await context.TruckModels
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ModelName == "UTEST " + entropy);

                Assert.IsNull(get_deleted_truck);
            }
        }
    }
}