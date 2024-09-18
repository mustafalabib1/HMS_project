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
    internal class ActiveSubstanceConfiguraton : IEntityTypeConfiguration<ActiveSubstance>
    {
        public void Configure(EntityTypeBuilder<ActiveSubstance> builder)
        {
            builder.HasKey(e => e.ActiveSubstancesId);

            builder.HasIndex(e => e.ActiveSubstancesName).IsUnique();
            builder.Property(e => e.ActiveSubstancesName).HasMaxLength(50);

            builder.HasMany(d => d.MedicationCodes).WithMany(p => p.ActiveSubstances);
        }
    }
}
