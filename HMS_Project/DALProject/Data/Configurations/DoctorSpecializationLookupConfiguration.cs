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
    internal class DoctorSpecializationLookupConfiguration : IEntityTypeConfiguration<DoctorSpecializationLookup>
    {
        public void Configure(EntityTypeBuilder<DoctorSpecializationLookup> builder)
        {
            #region configuration 
            builder.Property(s => s.Specialization).HasMaxLength(50);
            #endregion

            #region One2Many With Doctor
            builder
                    .HasMany(s => s.Doctors)
                    .WithOne(c => c.DoctorSpecialization)
                    .HasForeignKey(c => c.SpecializationId);
            #endregion
        }
    }
}
