using Examen_ASP_VanDoorenFabio_3IMDA_2018.Entities;
using Microsoft.EntityFrameworkCore;

namespace Examen_ASP_VanDoorenFabio_3IMDA_2018.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);

            modelBuilder.Entity<CarOwner>().HasKey(co => new {co.OwnerId, co.CarId});

            modelBuilder.Entity<Owner>().HasKey(o => o.Id);
            modelBuilder.Entity<Owner>().HasMany(c => c.Car).WithOne(co => co.Owner);

            modelBuilder.Entity<CarType>().HasKey(t => t.Id);
            modelBuilder.Entity<Car>().HasOne(t => t.Cartype).WithMany(c => c.Cars);
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> Cartype { get; set; }
    }
}
