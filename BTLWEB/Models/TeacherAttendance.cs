using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class TeacherAttendance
{
    public int Id { get; set; }

    public int? TeacherId { get; set; }

    public bool? Status { get; set; }

    public DateTime? Date { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
