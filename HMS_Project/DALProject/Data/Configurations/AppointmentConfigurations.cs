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
    internal class ApointmentConfigurations : IEntityTypeConfiguration<Apointment>
    {
        public void Configure(EntityTypeBuilder<Apointment> builder)
        {
            #region ApointmentConfiguration 
            builder.HasKey(a => a.Id);
            builder.Property(a => a.ApointmentDate).HasColumnType($"{DB_DataTypes_Helper.date}");
            builder.Property(a => a.ApointmentTime).HasColumnType($"{DB_DataTypes_Helper.time}");
            builder.Property(a => a.ApointmentStatus).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(15);
            builder.Property(a => a.Examination).HasMaxLength(100); 
            #endregion

            #region One2Many With Receptionist
            builder
                    .HasOne(a => a.Receptionist)
                    .WithMany(a => a.Apointments)
                    .HasForeignKey(a => a.ReceptionistId)
                    .OnDelete(DeleteBehavior.SetNull);
            #endregion

            #region One2Many With Patient
            builder
                    .HasOne(a => a.Patient)
                    .WithMany(a => a.Apointments)
                    .HasForeignKey(a => a.PatientId);
            #endregion

            #region One2Many With Clinic 
            builder
                    .HasOne(a => a.Clinic)
                    .WithMany(c => c.Apointments)
                    .HasForeignKey(c => c.ClinicId); 
            #endregion

            #region One2Many With doctor 
            builder 
                .HasOne(a=>a.Doctor)
                .WithMany(d=>d.Apointments)
                .HasForeignKey(a=>a.DoctorId);
            #endregion

            #region One2One With Prescription
            builder 
                .HasOne(a=>a.Prescription)
                .WithOne(p=>p.Apointment)
                .HasForeignKey<Apointment>(p=>p.Id)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region One2One With Invoice
            builder
                .HasOne(a=>a.Invoice)
                .WithOne(i=>i.Apointment)
                .HasForeignKey<Invoice>(i=>i.Id);
            #endregion
        }
    }
}
