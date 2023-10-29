using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLWEB.Models;

public partial class QuanLiSinhVienWebContext : DbContext
{
    public QuanLiSinhVienWebContext()
    {
    }

    public QuanLiSinhVienWebContext(DbContextOptions<QuanLiSinhVienWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherAttendance> TeacherAttendances { get; set; }

    public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }

    public virtual DbSet<WebUser> WebUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-B9POG429\\SQLEXPRESS;Initial Catalog=QuanLiSinhVienWeb;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8CA85FDB6");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            //thêm avatar
            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK__Admin__UserName__4BAC3F29");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927A0B878B961");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exam__297521C7C37A7E12");

            entity.ToTable("Exam");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.RollNo).HasMaxLength(20);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Class).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Exam__ClassID__6754599E");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Exam__SubjectID__68487DD7");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Expense__1445CFD34E2307B4");

            entity.ToTable("Expense");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Class).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Expense__ClassID__6B24EA82");

            entity.HasOne(d => d.Subject).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Expense__Subject__6C190EBB");
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.FeesId).HasName("PK__Fees__7BD9FD27A2AC367E");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");

            entity.HasOne(d => d.Class).WithMany(p => p.Fees)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Fees__ClassID__6477ECF3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A7964AB6773");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RollNo).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student__ClassID__534D60F1");
        });

        modelBuilder.Entity<StudentAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentA__3214EC073E918CB7");

            entity.ToTable("StudentAttendance");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.RollNo).HasMaxLength(20);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentAttendances)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__StudentAt__Class__60A75C0F");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentAttendances)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__StudentAt__Subje__619B8048");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__AC1BA3881B0037FC");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.SubjectName).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Subject__ClassID__5070F446");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teacher__EDF259444CB87D16");

            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK__Teacher__UserNam__5629CD9C");
        });

        modelBuilder.Entity<TeacherAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeacherA__3214EC077F195391");

            entity.ToTable("TeacherAttendance");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherAttendances)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__TeacherAt__Teach__5DCAEF64");
        });

        modelBuilder.Entity<TeacherSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeacherS__3214EC0786D919B1");

            entity.ToTable("TeacherSubject");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Class).WithMany(p => p.TeacherSubjects)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__TeacherSu__Class__59063A47");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__TeacherSu__Subje__59FA5E80");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherSubjects)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__TeacherSu__Teach__5AEE82B9");
        });

        modelBuilder.Entity<WebUser>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__WebUser__C9F28457482EF3C4");

            entity.ToTable("WebUser");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Loai).HasMaxLength(20);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Avatar).HasMaxLength(50);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
