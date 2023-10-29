using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class StudentAttendance
{
    public int Id { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public string? RollNo { get; set; }

    public bool? Status { get; set; }

    public DateTime? Date { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Subject? Subject { get; set; }
}
