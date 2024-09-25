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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.Specialization).HasMaxLength(50);

            #region User Configuration 
            builder.HasKey(u => u.SSN);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.SSN).ValueGeneratedNever();
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.Phone).HasMaxLength(20);
            builder.Property(e => e.UserPassword).HasMaxLength(50);
            builder.Property(p => p.Address).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(100); 
            #endregion
        }
    }
}
