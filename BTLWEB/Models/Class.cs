using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();

    public virtual ICollection<Expense> Expenses { get; } = new List<Expense>();

    public virtual ICollection<Fee> Fees { get; } = new List<Fee>();

    public virtual ICollection<StudentAttendance> StudentAttendances { get; } = new List<StudentAttendance>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; } = new List<Subject>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; } = new List<TeacherSubject>();
}
