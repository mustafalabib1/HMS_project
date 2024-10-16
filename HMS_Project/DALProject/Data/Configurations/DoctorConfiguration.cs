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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
			#region User Configuration 
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
			builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.SSN).ValueGeneratedNever();
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.FullName).HasMaxLength(100);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.Property(p => p.Address).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(100); 
            builder.Property(u=>u.Gender).HasMaxLength(10);
			builder.Property(e => e.Gender)
				.HasConversion((Gender) => Gender.ToString(),(genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true));
			#endregion
		}
    }
}
