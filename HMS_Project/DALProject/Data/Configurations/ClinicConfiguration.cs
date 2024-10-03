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
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(c => c.ClinicId)
                .UseIdentityColumn(10, 10);

            builder.Property(c => c.Phone)
                .HasColumnType($"{DB_DataTypes_Helper.nvarchar}")
                .HasMaxLength(20);
            
            #region One2Many with Nurses
            builder
                    .HasMany(c => c.Nurses)
                    .WithOne(n => n.Clinic)
                    .HasForeignKey(n => n.ClinicId)
                    .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region One2Many With Doctor
            builder
                    .HasMany(c => c.Doctors)
                    .WithOne(d => d.Clinic)
                    .HasForeignKey(d => d.ClinicId)
                    .OnDelete(DeleteBehavior.SetNull); 
            #endregion
        }
    }
}
