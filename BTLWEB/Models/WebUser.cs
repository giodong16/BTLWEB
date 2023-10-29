using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class WebUser
{
    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public string? Loai { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
