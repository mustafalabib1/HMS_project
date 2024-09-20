using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Data.Configurations
{
    internal class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.Property(p => p.PatAddress).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(100);
            builder.HasKey(e => e.PatientId);
            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.SSN)
                .ValueGeneratedNever();
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.Phone).HasMaxLength(20);
            builder.Property(e => e.UserPassword)
                .HasMaxLength(50);
            builder
                .HasMany(p=>p.PatientMedication)
                .WithOne()
                .HasForeignKey(p=> p.PatientPatientId);
            builder
                .HasMany(p => p.Prescriptions)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);
            builder
                .HasMany(p => p.Apointments)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);
            builder
                .HasMany(p => p.Invoices)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientId);
            builder
                .HasMany(p => p.MedicalRecords)
                .WithOne(p => p.Patient)
                .HasForeignKey(p => p.PatientID);
        }
    }
}
