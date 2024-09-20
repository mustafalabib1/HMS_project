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
           
            builder.Property(c => c.Phone).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(20);
            builder
                .HasMany(c => c.invoices)
                .WithOne(c => c.Reception)
                .HasForeignKey(c => c.ReceptionId);
            builder
                .HasMany(c => c.Receptionists)
                .WithOne(c => c.Reception)
                .HasForeignKey(c => c.ReceptionID);
            builder
                .HasMany(c => c.Apointments)
                .WithOne(c => c.Reception)
                .HasForeignKey(c => c.ReceptionId);
        }
    }
}
