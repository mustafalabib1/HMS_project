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
    internal class ReceptionistConfigurations : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
			#region User Configuration 
			builder.HasKey(a => a.UserId);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
			#endregion
		}
	}
}
