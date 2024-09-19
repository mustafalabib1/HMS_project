using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Configurations
{
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasOne(p => p.Pharmacy).WithMany(p => p.Prescriptions).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
