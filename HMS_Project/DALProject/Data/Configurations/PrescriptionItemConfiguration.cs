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
    internal class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
    {
        public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
        {
			#region PrescriptionItem Configuration
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
			builder.Property(p => p.FullDosage).HasMaxLength(100);
            #endregion

            #region One2Many With Prescription
            builder
                .HasOne(p => p.Prescription)
                .WithMany(p => p.PrescriptionItems)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region One2One With active Substance
            builder 
                .HasOne(p=>p.ActiveSubstance)
                .WithOne(p=>p.PatrescriptionItem)
                .HasForeignKey<PrescriptionItem>(p=>p.ActiveSubstanceId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
        }
    }
}
