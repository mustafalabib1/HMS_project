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
    internal class ClinicSpecializationLookupConfiguratoin : IEntityTypeConfiguration<ClinicSpecializationLookup>
    {
        public void Configure(EntityTypeBuilder<ClinicSpecializationLookup> builder)
        {
            #region configuration 
            builder.HasKey(s => s.Specialization);
            builder.Property(s => s.Specialization).HasMaxLength(50); 
            #endregion

            #region One2Many With clinic
            builder
                    .HasMany(s => s.Clinic)
                    .WithOne(c => c.ClinicSpecilization)
                    .HasForeignKey(c => c.Specilization); 
            #endregion

        }
    }
}
