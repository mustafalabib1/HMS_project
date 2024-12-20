﻿using DALProject.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.Data.Configurations
{
    internal class DoctorScheduleLookupConfiguration : IEntityTypeConfiguration<DoctorScheduleLookup>
    {
        public void Configure(EntityTypeBuilder<DoctorScheduleLookup> builder)
        {
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
            builder.Property(ds=> ds.Day).HasMaxLength(10);

            builder.Property(d => d.Day)
                .HasConversion((DayOfWeek) => DayOfWeek.ToString(), (DayOfWeekAsString) => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayOfWeekAsString, true));


            #region One2Many With doctor
            builder
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorScheduleLookups)
                .HasForeignKey(ds => ds.DoctorUserId);
            #endregion
        }
    }
}
