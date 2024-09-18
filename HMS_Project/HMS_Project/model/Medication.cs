using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class Medication
{
    public string MedicationCode { get; set; } = null!;

    public string MedName { get; set; } = null!;

    public int Strength { get; set; }
    public virtual ICollection<ActiveSubstance> ActiveSubstances { get; set; } = new List<ActiveSubstance>();
    public virtual ICollection<PatientMedication> PatientMedications { get; set; } = new List<PatientMedication>();
    public virtual Pharmacy Pharmacy { get; set; } = null!;

}
