using System;
using System.Collections.Generic;

namespace DALProject.model;

public class ActiveSubstanceInteraction : ModelBase
{
    public string? Interaction { get; set; } = null!;

    #region Many2Many With ActiveSbustance
    public int? ActiveSubstanceId1 { get; set; }
    public virtual ActiveSubstance ActSub1 { get; set; } = null!;
    public int? ActiveSubstanceId2 { get; set; }
    public virtual ActiveSubstance ActSub2 { get; set; } = null!; 
    #endregion
}
