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
    internal class ActiveSubstanceConfiguraton : IEntityTypeConfiguration<ActiveSubstance>
    {
        public void Configure(EntityTypeBuilder<ActiveSubstance> builder)
        {
            #region Active Substance Configuration 
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.ActiveSubstancesName).IsUnique();
            builder.Property(e => e.ActiveSubstancesName).HasMaxLength(50);
            #endregion

            #region Many2Many With Medication 
            builder
                    .HasMany(d => d.Medications)
                    .WithMany(p => p.ActiveSubstances); 
            #endregion
        }
    }
}
