using Microsoft.EntityFrameworkCore;
using FisioScan.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace FisioScan.Data
{
    public class FisioScanContext : DbContext
    {
        public FisioScanContext(DbContextOptions<FisioScanContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Actualizamos el objeto Physio con la propiedad Email y LastName.
            modelBuilder.Entity<Physio>().HasData(
                new Physio 
                { 
                    PhysioId = 1, 
                    Name = "Pakito", 
                    LastName = "Perez", 
                    Email = "pakito.perez@example.com", // Nueva propiedad Email
                    RegistrationNumber = 1783, 
                    Password = "1234" 
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, Name = "John Doe", Dni = "730151515" },
                new Patient { PatientId = 2, Name = "Pedro Martínez", Dni = "730203040" }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Physio>? Physios { get; set; }
    }
}

