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
    internal class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
			#region Mediction configuration 
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
			builder.Property(e => e.MedName).HasMaxLength(50);
            #endregion
        }
    }
}
