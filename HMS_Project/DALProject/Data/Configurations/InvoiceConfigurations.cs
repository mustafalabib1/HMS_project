using DALProject.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.Data.Configurations
{
    internal class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            #region Invoice Configuration 
            builder
                    .Property(i => i.InvoiceDate)
                    //.HasColumnType(DataType.DateTime)
                    .HasComputedColumnSql("GETDATE()");

            builder
                .Property(i => i.TotalAmount)
                .HasColumnType("decimal(12,2)");

            builder
                .Property(i => i.PaymentStatus)
                .HasColumnType($"{DB_DataTypes_Helper.bit}");

            builder
                .Property(i => i.PaymentType)
                .HasColumnType($"{DB_DataTypes_Helper._char}")
                .HasMaxLength(1);

            builder.Property(i => i.PaymentType).HasMaxLength(15);
            #endregion

            #region One2Many With Receptionist
            builder
                   .HasOne(i => i.Receptionist)
                   .WithMany(i => i.invoices)
                   .HasForeignKey(i => i.ReceptionistId); 
            #endregion
        }
    }
}
