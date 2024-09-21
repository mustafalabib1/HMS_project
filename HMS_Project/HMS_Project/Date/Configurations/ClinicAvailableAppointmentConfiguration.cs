using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Date.Configurations
{
    internal class ClinicAvailableAppointmentConfiguration : IEntityTypeConfiguration<ClinicAvailableAppointment>
    {
        public void Configure(EntityTypeBuilder<ClinicAvailableAppointment> builder)
        {
            builder
                .HasKey(ca => new { ca.ClinicId, ca.AvailableAppointmentId });

            builder
                .HasOne(ca => ca.Clinic)
                .WithMany(c => c.ClinicAvailableAppointments)
                .HasForeignKey(ca => ca.ClinicId);

            builder
                .HasOne(ca => ca.AvailableAppointment)
                .WithMany(aa => aa.ClinicAvailableAppointments)
                .HasForeignKey(ca => ca.AvailableAppointmentId);
        }
    }
}
