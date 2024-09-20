using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Date.Configurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(c => c.Phone).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(20);
            builder
                .HasMany(c => c.Nurses)
                .WithOne(n => n.Clinic)
                .HasForeignKey(n => n.ClinicId);
            builder
                .HasMany(c => c.Doctors)
                .WithOne(d => d.Clinic)
                .HasForeignKey(d => d.ClinicId);
            builder
                .HasMany(c => c.Apointments)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicId);
        }
    }
}
