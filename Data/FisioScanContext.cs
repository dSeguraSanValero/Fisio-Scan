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
            modelBuilder.Entity<Physio>()
                .Property(p => p.Role)
                .HasDefaultValue("Physio");

            modelBuilder.Entity<Physio>().HasData(
                new Physio 
                { 
                    PhysioId = 1, 
                    Name = "Juan",
                    FirstSurname = "Perez",
                    SecondSurname = "Martínez",
                    Email = "admin@admin.com",
                    RegistrationNumber = 1568,
                    Password = "admin",
                    Role = "Admin"
                },
                new Physio 
                { 
                    PhysioId = 2, 
                    Name = "David", 
                    FirstSurname = "Calvo",
                    SecondSurname = "Alonso",
                    Email = "pakito.perez@example.com",
                    RegistrationNumber = 1247, 
                    Password = "1234",
                    Role = "Physio"
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient 
                { 
                    PatientId = 1,
                    CreatedBy = 1, 
                    Name = "John",
                    FirstSurname = "González",
                    SecondSurname = "Rodríguez",
                    Dni = "724264567" 
                },
                new Patient 
                { 
                    PatientId = 2,
                    CreatedBy = 2, 
                    Name = "Luis",
                    FirstSurname = "Sánchez",
                    SecondSurname = "Martínez",
                    Dni = "723626246" 
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physio> Physios { get; set; }
    }
}

