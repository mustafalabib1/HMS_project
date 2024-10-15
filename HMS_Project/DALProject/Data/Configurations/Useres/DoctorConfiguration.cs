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
            builder.HasBaseType<AppUser>();
            builder.Property(e => e.SSN).HasMaxLength(20);
            builder.Property(e => e.FullName).HasMaxLength(100);
            builder.Property(p => p.Address).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(100); 
            builder.Property(u=>u.Gender).HasMaxLength(10);
			builder.Property(e => e.Gender)
				.HasConversion((Gender) => Gender.ToString(),(genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true));
			#endregion
		}
    }
}
