using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Fee
{
    public int FeesId { get; set; }

    public int? ClassId { get; set; }

    public int? FeesAmount { get; set; }

    public virtual Class? Class { get; set; }
}
