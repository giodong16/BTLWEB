using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? UserName { get; set; }

    public string? Name { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public long? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual WebUser? UserNameNavigation { get; set; }
}
