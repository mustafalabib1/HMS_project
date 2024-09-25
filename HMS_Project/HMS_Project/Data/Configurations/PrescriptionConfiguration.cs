using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Data.Configurations
{
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            #region Presciption Configuration 
            builder.Property(p => p.Dosage).HasMaxLength(50);
            #endregion

            #region Many2Many With Active Substance 
            builder
                .HasMany(p => p.activeSubstances)
                .WithMany(a => a.Prescriptions);
            #endregion

            #region Many2Many With Medicaion 
            builder
                .HasMany(p => p.Medications)
                .WithMany(m=>m.Prescriptions);
            #endregion

            #region One2Many With Pharmacy 
            builder
                    .HasOne(p => p.Pharmacy)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p=>p.PharmacyId)
                    .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region One2Many With Doctor
            builder
                    .HasOne(p=>p.Doctor)
                    .WithMany(d=> d.Prescriptions)
                    .HasForeignKey(n => n.DoctorId);
            #endregion

        }
    }
}
