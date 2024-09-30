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
    internal class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            #region AppointmentConfiguration 
            builder.HasKey(a => a.AppointmentId);
            builder.Property(a => a.AppointmentDate).HasColumnType($"{DB_DataTypes_Helper.date}");
            builder.Property(a => a.AppointmentTime).HasColumnType($"{DB_DataTypes_Helper.time}");
            builder.Property(a => a.AppointmentStatus).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(15);
            builder.Property(a => a.Examination).HasMaxLength(100); 
            #endregion

            #region One2Many With Receptionist
            builder
                    .HasOne(a => a.Receptionist)
                    .WithMany(a => a.Appointments)
                    .HasForeignKey(a => a.ReceptionistId)
                    .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region One2Many With Patient
            builder
                    .HasOne(a => a.Patient)
                    .WithMany(a => a.Appointments)
                    .HasForeignKey(a => a.PatientId);
            #endregion

            #region One2Many With Clinic 
            builder
                    .HasOne(a => a.Clinic)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(c => c.ClinicId)
                    .OnDelete(DeleteBehavior.SetNull); 
            #endregion

            #region One2Many With doctor 
            builder 
                .HasOne(a=>a.Doctor)
                .WithMany(d=>d.Appointments)
                .HasForeignKey(a=>a.DoctorId);
            #endregion

            #region One2One With Prescription
            builder 
                .HasOne(a=>a.Prescription)
                .WithOne(p=>p.Appointment)
                .HasForeignKey<Appointment>(p=>p.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region One2One With Invoice
            builder
                .HasOne(a=>a.Invoice)
                .WithOne(i=>i.Appointment)
                .HasForeignKey<Invoice>(i=>i.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
        }
    }
}
