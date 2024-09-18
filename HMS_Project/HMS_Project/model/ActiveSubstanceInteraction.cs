using System;
using System.Collections.Generic;

namespace HMS_Project.model;

public partial class ActiveSubstanceInteraction
{
    public int ActiveSubstanceId1 { get; set; }

    public int ActiveSubstanceId2 { get; set; }

    public string? Interaction { get; set; }

    public virtual ActiveSubstance ActSub1 { get; set; } = null!;

    public virtual ActiveSubstance ActSub2 { get; set; } = null!;
}
