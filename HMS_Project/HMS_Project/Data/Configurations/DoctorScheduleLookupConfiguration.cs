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
    internal class DoctorScheduleLookupConfiguration : IEntityTypeConfiguration<DoctorScheduleLookup>
    {
        public void Configure(EntityTypeBuilder<DoctorScheduleLookup> builder)
        {
            builder.HasKey(ds => new { ds.Day, ds.DoctorId });

            #region One2Many With doctor
            builder
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorScheduleLookups)
                .HasForeignKey(ds => ds.DoctorId);
            #endregion
        }
    }
}
