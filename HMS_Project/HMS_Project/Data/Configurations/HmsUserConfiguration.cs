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
    internal class HmsUserConfiguration : IEntityTypeConfiguration<HmsUser>
    {
        public void Configure(EntityTypeBuilder<HmsUser> builder)
        {
            builder.HasKey(e => e.SSN);
            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.SSN)
                .ValueGeneratedNever();
            builder.Property(e => e.Email).HasMaxLength(50);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.Phone).HasMaxLength(20);
            builder.Property(e => e.UserPassword)
                .HasMaxLength(50);
        }
    }
}
