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
    internal class ReceptionConfigurations : IEntityTypeConfiguration<Reception>
    {
        public void Configure(EntityTypeBuilder<Reception> builder)
        {
            builder.HasKey(r => r.ReceptionID);
            builder.Property(r => r.phone).HasColumnType($"{DB_DataTypes_Helper.nvarchar}").HasMaxLength(20);
        }
    }
}
