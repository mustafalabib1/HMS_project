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
            #region One2Many With Pharmacist 
            builder
                    .HasOne(p => p.Pharmacist)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p=>p.PharmacistId)
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
