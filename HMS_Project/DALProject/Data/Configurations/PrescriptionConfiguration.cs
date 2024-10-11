using DALProject.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.Data.Configurations
{
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
			#region One2Many With Pharmacist 
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
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
