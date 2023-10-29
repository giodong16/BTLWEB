using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public string? RollNo { get; set; }

    public int? TotalMarks { get; set; }

    public int? OutOfMarks { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Subject? Subject { get; set; }
}
