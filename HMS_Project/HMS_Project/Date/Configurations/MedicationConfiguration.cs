using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Configurations
{
    internal class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasKey(e => e.MedicationCode);
            builder.Property(e => e.MedicationCode).HasMaxLength(20);
            builder.Property(e => e.MedName).HasMaxLength(50);

            builder.HasOne(d => d.Pharmacy).WithMany(d => d.Medications).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasMany(m => m.PatientMedications).WithOne().HasForeignKey(p=>p.MedicationMedicationCode);
        }
    }
}
