﻿using DALProject.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.Data.Configurations
{
    internal class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
			#region User Configuration 
			builder.HasKey(a => a.UserId);
			builder.Property(a => a.Id).UseIdentityColumn(1, 1);
			#endregion
		}
	}
}
