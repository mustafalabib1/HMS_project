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
    internal class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            #region Pharmacy Configuration 
            builder.Property(c => c.Phone).HasMaxLength(20);
            builder.Property(p=>p.PharmacyName).HasMaxLength(20);
            #endregion

            #region One2Many With Pharmacist
            builder
                .HasMany(p => p.Pharmacists)
                .WithOne(p => p.Pharmacy)
                .HasForeignKey(p => p.PharmacyId);
            #endregion
        }
    }
}
