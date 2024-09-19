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
    internal class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(m => m.RecordID);
            builder.Property(m => m.Diagnosis).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(250);
            builder.Property(m => m.CreatedDate).HasComputedColumnSql("GETDATE()");
            builder.Property(m => m.LabResults).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(250);
        }
    }
}
