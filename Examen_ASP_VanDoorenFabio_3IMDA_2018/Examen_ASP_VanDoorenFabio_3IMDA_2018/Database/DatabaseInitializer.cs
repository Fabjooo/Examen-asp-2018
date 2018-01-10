using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {
            if (((entityContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }

            var owners = new List<Owner>();
            owners.Add(new Owner {FirstName = $"Fabio", LastName = $"Van Dooren"});
            owners.Add(new Owner { FirstName = $"Nico", LastName = $"Styfhals" });
            owners.Add(new Owner { FirstName = $"Andreas", LastName = $"Van Den Driessche" });
            owners.Add(new Owner { FirstName = $"Michee", LastName = $"Kalamba" });
            owners.Add(new Owner { FirstName = $"Maaike", LastName = $"Goossens" });
            owners.Add(new Owner { FirstName = $"", LastName = $"" });
                

            var cartypes = new List<CarType>
            {
                new CarType() {Brand = "BMW", Model = "M3 Telesto"},
                new CarType() {Brand = "BMW", Model = "M3 30 jahre"},
                new CarType() {Brand = "Mini", Model = "Cooper S"},
                new CarType() {Brand = "Mercedes", Model = "S500"},
                new CarType() {Brand = "Hyundai", Model = "i30 N"},
                new CarType() {Brand = "BMW", Model = "1er M Coupé"},
                new CarType() {Brand = "BMW", Model = "318i"}
            };

            var cars = new List<Car>();
            for (var i = 0; i < 5; i++)
            {
                var carOwner = new CarOwner()
                {
                    Owner = owners[i]
                };

                CarType cartype = null;
                if (i % 4 == 0)
                {
                    cartype = cartypes[0];
                }
                else if (i % 3 == 0)
                {
                    cartype = cartypes[1];
                }
                else if (i % 2 == 0)
                {
                    cartype = cartypes[2];
                }
                else {
                    cartype = cartypes[3];
                }

                cars.Add(new Car
                {
                    Color = $"Grey",
                    LicensePlate = $"1-CRY-777",
                    DatePurchased = new DateTime(2015, 02, 19),
                    Owner = new List<CarOwner>() { carOwner },
                    Cartype = cartype
                });
            }

            entityContext.Database.EnsureCreated();
            entityContext.Owners.AddRange(owners);
            entityContext.Cartype.AddRange(cartypes);
            entityContext.Cars.AddRange(cars);
            entityContext.SaveChanges();
        }
    }
}