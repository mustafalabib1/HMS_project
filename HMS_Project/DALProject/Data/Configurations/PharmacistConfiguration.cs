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
    internal class PharmacistConfiguration : IEntityTypeConfiguration<Pharmacist>
    {
        public void Configure(EntityTypeBuilder<Pharmacist> builder)
        {
            #region User Configuration 
             
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.SSN).ValueGeneratedNever();
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.FullName).HasMaxLength(100);
            builder.Property(e => e.Phone).HasMaxLength(20);
            builder.Property(e => e.UserPassword).HasMaxLength(50);
            builder.Property(p => p.Address).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(100);
            builder.Property(u => u.Gender).HasMaxLength(10);
            #endregion
        }
    }
}
