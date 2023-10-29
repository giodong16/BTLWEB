using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public int? ClassId { get; set; }

    public string? SubjectName { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();

    public virtual ICollection<Expense> Expenses { get; } = new List<Expense>();

    public virtual ICollection<StudentAttendance> StudentAttendances { get; } = new List<StudentAttendance>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; } = new List<TeacherSubject>();
}
