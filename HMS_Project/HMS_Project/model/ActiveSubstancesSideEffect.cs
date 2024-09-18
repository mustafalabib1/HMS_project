using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class ActiveSubstancesSideEffect
{
    public string SideEffects { get; set; } = null!;

    public int ActiveSubstancesId { get; set; }

    public virtual ActiveSubstance ActiveSubstances { get; set; } = null!;
}
