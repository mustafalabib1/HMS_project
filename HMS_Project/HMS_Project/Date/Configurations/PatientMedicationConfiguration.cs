using HMS_Project.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Configurations
{
    internal class PatientMedicationConfiguration : IEntityTypeConfiguration<PatientMedication>
    {
        public void Configure(EntityTypeBuilder<PatientMedication> builder)
        {
            builder.HasKey(pm=> new { pm.PatientPatientId, pm.MedicationMedicationCode });
        }
    }
}
