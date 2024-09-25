using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class ActiveSubstance
{
    public int ActiveSubstancesId { get; set; }
    public string ActiveSubstancesName { get; set; } = null!;

    #region Many2Many With Medication
    public virtual ICollection<Medication> Medications { get; set; } = new HashSet<Medication>();
    #endregion
    
    #region One2Many With ActiveSubstanceInteraction
    public virtual ICollection<ActiveSubstanceInteraction> ActSub1 { get; set; } = new HashSet<ActiveSubstanceInteraction>();
    public virtual ICollection<ActiveSubstanceInteraction> ActSub2 { get; set; } = new HashSet<ActiveSubstanceInteraction>();
    #endregion
    
    #region Many2Many With Patient
    public virtual ICollection<Patient> PatientshaveAllergy { get; set; } = new HashSet<Patient>();
    #endregion

    #region One2One With PrescriptionItem
    public virtual PrescriptionItem PatrescriptionItem { get; set; } = null!;
    #endregion
}
