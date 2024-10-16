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
	internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasKey(u => u.Id);

			builder.HasOne(u => u.Doctor)
				   .WithOne(d => d.AppUser)
				   .HasForeignKey<Doctor>(d => d.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(u => u.Patient)
				   .WithOne(d => d.AppUser)
				   .HasForeignKey<Patient>(d => d.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(u => u.Nurse)
				   .WithOne(d => d.AppUser)
				   .HasForeignKey<Nurse>(d => d.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(u => u.Pharmacist)
				   .WithOne(d => d.AppUser)
				   .HasForeignKey<Pharmacist>(d => d.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(u => u.Receptionist)
				   .WithOne(d => d.AppUser)
				   .HasForeignKey<Receptionist>(d => d.UserId)
				   .OnDelete(DeleteBehavior.SetNull);

		}
	}
}
