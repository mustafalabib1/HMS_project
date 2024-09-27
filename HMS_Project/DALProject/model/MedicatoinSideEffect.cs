using System;
using System.Collections.Generic;

namespace DALProject.model;

public partial class MedicatoinSideEffect
{
    public string SideEffects { get; set; } = null!;

    #region One2Many With Medication
    public string? MedicationCode { get; set; } = null!;
    public virtual Medication Medication { get; set; } = null!; 
    #endregion
}
