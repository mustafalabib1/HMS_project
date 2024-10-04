using DALProject.model;
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
namespace DALProject.Data.Contexts
{
    public class HMSdbcontext : DbContext
    {
        public HMSdbcontext(DbContextOptions<HMSdbcontext> options):base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=DESKTOP-9OPA1VT\\MSS;Database=HMS02;Trusted_Connection=True;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");/*MultipleActiveResultSets=True;*/
            //optionsBuilder.UseSqlServer("Server=DESKTOP-F0FJTS7\\SQLEXPRESS;Database=HMS02;Trusted_Connection=True;Encrypt=False"); connection for 7awy

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Pharmacist)
                .WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public virtual DbSet<Patient> Patients { get; set; } // Patient table inherit from User (TPC)
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Apointment> Apointments { get; set; }
        public virtual DbSet<Receptionist> Receptionists { get; set; }//Receptionists  table inherit from User (TPC)


        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; }
        public virtual DbSet<Medication> Medication { get; set; }
        public virtual DbSet<Pharmacist> Pharmacists { get; set; } //Pharmacists  table inherit from User (TPC)
        public virtual DbSet<Prescription> Prescriptions { get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; } //Doctors  table inherit from User (TPC)
        public virtual DbSet<DoctorScheduleLookup> DoctorScheduleLookups { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; } //Nurses  table inherit from User (TPC)
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<ClinicSpecializationLookup> ClinicsSpecializationLookups { get;  set; }
        public virtual DbSet<DoctorSpecializationLookup> DoctorSpecializationLookup { get; set; }

    }
}
