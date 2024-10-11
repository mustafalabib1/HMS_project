using DALProject.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.Data.Configurations
{
    internal class PrescriptionItemMedicationConfiguration : IEntityTypeConfiguration<PrescriptionItemMedication>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItemMedication> builder)
        {
            #region PrescriptionItemMedicationConfiguration
            builder.HasKey(p => new { p.MedicationId, p.PrescriptionItemId });

            builder.Property(p => p.Dosage).HasMaxLength(100);
            builder.Property(p => p.Duration).HasMaxLength(100);
            #endregion

            #region One2Many With Medicatoin
            builder
                .HasOne(p => p.Medication)
                .WithMany(p => p.PrescriptionItemMedications);
            #endregion

            #region One2Many With PrescriptionItem
            builder
                .HasOne(p => p.PrescriptionItem)
                .WithMany(p => p.Medications);
            #endregion
        }
    }
}
