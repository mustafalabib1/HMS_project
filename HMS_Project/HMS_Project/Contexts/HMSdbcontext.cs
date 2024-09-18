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
            optionsBuilder.UseSqlServer("Server=DESKTOP-AM9IO3H\\MSSQLSERVER01;Database=HMS02;Trusted_Connection=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Prescription>()
                        .HasOne(p => p.Pharmacy)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(p => p.PharmacyId);
            modelBuilder.Entity<Pharmacist>()
                        .HasOne(p => p.Pharmacy)
                        .WithMany(p => p.Pharmacists)
                        .HasForeignKey(p => p.PharmacyId);
        }

        public virtual DbSet<HmsUser> HmsUsers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

    }
}
