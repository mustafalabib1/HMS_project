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
    internal class ReceptionistConfigurations : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
