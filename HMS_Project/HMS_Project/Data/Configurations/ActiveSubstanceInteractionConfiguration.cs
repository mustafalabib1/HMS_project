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
    internal class ActiveSubstanceInteractionConfiguration : IEntityTypeConfiguration<ActiveSubstanceInteraction>
    {
        public void Configure(EntityTypeBuilder<ActiveSubstanceInteraction> builder)
        {
            #region ActiveSubstanceInteraction Configuration
            builder.HasKey(e => new { e.ActiveSubstanceId1, e.ActiveSubstanceId2 });
            builder.Property(e => e.Interaction).HasMaxLength(100);
            #endregion

            #region One2Many With ActiveSubstanceInteraction
            builder
                    .HasOne(d => d.ActSub1).WithMany(p => p.ActSub1).
                    HasForeignKey(d => d.ActiveSubstanceId1)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.ActSub2).WithMany(p => p.ActSub2).HasForeignKey(d => d.ActiveSubstanceId2)
                .OnDelete(DeleteBehavior.ClientSetNull);  
            #endregion
        }
    }
}
