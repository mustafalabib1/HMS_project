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
    internal class ReceptionConfigurations : IEntityTypeConfiguration<Reception>
    {
        public void Configure(EntityTypeBuilder<Reception> builder)
        {
           
            builder.Property(r => r.Phone).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(20);
            builder
                .HasMany(r => r.invoices)
                .WithOne(r => r.Reception)
                .HasForeignKey(r => r.ReceptionId);
            builder
                .HasMany(r => r.Receptionists)
                .WithOne(r => r.Reception)
                .HasForeignKey(r => r.ReceptionID);
            builder
                .HasMany(r => r.Apointments)
                .WithOne(r => r.Reception)
                .HasForeignKey(r => r.ReceptionId);
        }
    }
}
