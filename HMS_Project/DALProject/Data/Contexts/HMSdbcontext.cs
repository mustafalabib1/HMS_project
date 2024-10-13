using DALProject.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public partial class HMSdbcontext : IdentityDbContext<IdentityUser>
    {
        public HMSdbcontext(DbContextOptions<HMSdbcontext> options) : base(options)
        {
        }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Pharmacist)
                .WithMany(p => p.Prescriptions)
                .OnDelete(DeleteBehavior.NoAction);
         }
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public virtual DbSet<AppUser> AppUsers { get; set; }

        public virtual DbSet<Patient> Patients { get; set; } // Patient table inherit from User (TPC)
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Apointment> Apointments { get; set; }
        public virtual DbSet<Receptionist> Receptionists { get; set; }//Receptionists  table inherit from User (TPC)


        public virtual DbSet<ActiveSubstance> ActiveSubstances { get; set; }
        public virtual DbSet<Medication> Medication { get; set; }
        public virtual DbSet<Pharmacist> Pharmacists { get; set; } //Pharmacists  table inherit from User (TPC)
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionItemMedication> PrescriptionItemMedication { get; set; }
        public virtual DbSet<PrescriptionItem> PrescriptionItem {  get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; } //Doctors  table inherit from User (TPC)
        public virtual DbSet<DoctorScheduleLookup> DoctorScheduleLookups { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; } //Nurses  table inherit from User (TPC)
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<ClinicSpecializationLookup> ClinicsSpecializationLookups { get; set; }
        public virtual DbSet<DoctorSpecializationLookup> DoctorSpecializationLookup { get; set; }

    }
}
