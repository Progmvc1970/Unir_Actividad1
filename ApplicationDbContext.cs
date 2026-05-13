using Azure;
using Microsoft.EntityFrameworkCore;
using MinimalApiGestióndeAlquilerdeVehículos.Entidades;
//using MinimalApiGestióndeAlquilerdeVehículos.Migrations;

namespace MinimalApiGestióndeAlquilerdeVehículos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected ApplicationDbContext()
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.Marca).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Modelo).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Placa).IsRequired().HasMaxLength(20);
                entity.Property(v => v.Estado).HasMaxLength(50);
                // Aquí se define la precisión del decimal
                entity.Property(v => v.PrecioPorDia).HasPrecision(18, 2);

                // Relación con Operaciones
               // entity.HasMany(v => v.Alquileres).WithOne(o => o.Vehiculo).HasForeignKey(o => o.VehiculoId);
            });

            // Configuración para Alquiler
            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.FechaInicio).IsRequired();
                entity.Property(o => o.FechaFin).IsRequired();
                entity.Property(o => o.Total).HasPrecision(18, 2);
            });
        }
        

        public DbSet<Cliente> Clientes { get; set; }  
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }
    }
}
