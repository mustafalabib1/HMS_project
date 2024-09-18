using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class ActiveSubstance
{
    public int ActiveSubstancesId { get; set; }

    public string ActiveSubstancesName { get; set; } = null!;

    public virtual ICollection<Medication> MedicationCodes { get; set; } = new HashSet<Medication>();
    public virtual ICollection<ActiveSubstancesSideEffect> ActiveSubstancesSideEffects { get; set; } = new HashSet<ActiveSubstancesSideEffect>();
    public virtual ICollection<ActiveSubstanceInteraction> ActSub1 { get; set; } = new HashSet<ActiveSubstanceInteraction>();
    public virtual ICollection<ActiveSubstanceInteraction> ActSub2 { get; set; } = new HashSet<ActiveSubstanceInteraction>();
    public virtual ICollection<Patient> Patients { get; set; } = new HashSet<Patient>();
    public virtual ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
}
