using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Contexts
{
    internal class HMSdbcontext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-AM9IO3H\\MSSQLSERVER01;Database=HMS01;Trusted_Connection=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HmsUser>().HasKey(u => u.SSN);
        }
        //public virtual DbSet<ActiveSubstances> ActiveSubstances { get; set; }
        //public virtual DbSet<ActiveSubstance_Interaction> ActiveSubstanceInteractions { get; set; }
        //public virtual DbSet<ActiveSubstances_SideEffects> ActiveSubstancesSideEffects { get; set; }
        public virtual DbSet<HmsUser> HmsUsers { get; set; }
        //public virtual DbSet<Medication> Medications { get; set; }
        //public virtual DbSet<Medications_ActiveSubstance> Medications_ActiveSubstance {  get; set; }
        //public virtual DbSet<Patient_ActiveSubstances_Allergies> Patient_ActiveSubstances_Allergies { get; set; }
        //public virtual DbSet<Patient_Medications> Patient_Medications { get; set; }
        //public virtual DbSet<Pharmacist> Pharmacists { get; set; }
        //public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        //public virtual DbSet<Prescription> Prescriptions { get; set; }
    }
}
