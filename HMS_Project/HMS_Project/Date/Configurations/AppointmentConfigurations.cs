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
    internal class AppointmentConfigurations : IEntityTypeConfiguration<Apointment>
    {
        public void Configure(EntityTypeBuilder<Apointment> builder)
        {
            builder.HasKey(a => a.ApointmentId);
            builder.Property(a => a.ApointmentDate).HasColumnType($"{DB_DataTypes_Helper.date}");
            builder.Property(a => a.ApointmentTime).HasColumnType($"{DB_DataTypes_Helper.time}");
            builder.Property(a => a.ApointmentStatus).HasColumnType($"{DB_DataTypes_Helper._char}").HasMaxLength(1);

            builder
                .HasOne(a => a.Reception)
                .WithMany(a => a.Apointments)
                .HasForeignKey(a => a.ReceptionId);
            builder
                .HasOne(a => a.Patient)
                .WithMany(a => a.Apointments)
                .HasForeignKey(a => a.PatientId);
        }
    }
}
