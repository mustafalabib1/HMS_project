using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace HMS_Project.Contexts
{
    internal class HMSdbcontext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9OPA1VT\\MSS;Database=HMS02;Trusted_Connection=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Pharmacy)
                .WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.NoAction);
            
                     
        }

<<<<<<< HEAD
        //public virtual DbSet<HmsUser> HmsUsers { get; set; }
        public virtual DbSet<Patient>Patients  { get; set; } // patient table inherit from User (TPC)
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; } 
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Apointment> Apointments { get; set; }
        public virtual DbSet<Reception> Reception { get; set; }
        public virtual DbSet<Receptionist> Receptionists { get; set; }


=======
        public virtual DbSet<HmsUser> HmsUsers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }
>>>>>>> 73fe4018a6f5031d7e6fa731359dd9b01e14d287

    }
}
