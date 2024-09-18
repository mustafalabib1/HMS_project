using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class PatientMedication
{
    public DateOnly DateIssued { get; set; }

    public int PatientPatientId { get; set; }
    public string MedicationMedicationCode { get; set; } = null!;
}
