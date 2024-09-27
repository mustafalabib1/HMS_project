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
    internal class MedicatoinSideEffectConfiguration : IEntityTypeConfiguration<MedicatoinSideEffect>
    {
        public void Configure(EntityTypeBuilder<MedicatoinSideEffect> builder)
        {
            #region Medication Configuration
            builder.HasKey(e => new { e.SideEffects, e.MedicationCode });
            builder.Property(e => e.SideEffects).HasMaxLength(250);
            builder.HasOne(d => d.Medication).WithMany(p => p.MedicatoinSideEffects)
                .HasForeignKey(d => d.MedicationCode)
                .OnDelete(DeleteBehavior.ClientSetNull); 
            #endregion
        }
    }
}
