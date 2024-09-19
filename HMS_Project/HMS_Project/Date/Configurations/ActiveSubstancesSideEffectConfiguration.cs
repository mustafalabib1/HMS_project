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
    internal class ActiveSubstancesSideEffectConfiguration : IEntityTypeConfiguration<ActiveSubstancesSideEffect>
    {
        public void Configure(EntityTypeBuilder<ActiveSubstancesSideEffect> builder)
        {
            builder.HasKey(e => new { e.SideEffects, e.ActiveSubstancesId });
            builder.Property(e => e.SideEffects).HasMaxLength(250);
            builder.Property(e => e.ActiveSubstancesId).HasColumnName("ActiveSubstancesID");

            builder.HasOne(d => d.ActiveSubstances).WithMany(p => p.ActiveSubstancesSideEffects)
                .HasForeignKey(d => d.ActiveSubstancesId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
