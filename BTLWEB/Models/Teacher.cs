using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLWEB.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? UserName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Họ tên phải có ít nhất 4 ký tự và tối đa 50 ký tự.")]
    [Display(Name = "Họ tên")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
    [Range(typeof(DateTime), "1/1/1960", "12/31/1995", ErrorMessage = "Ngày sinh phải trong khoảng 1/1/1960 - 12/31/1995")]
    [Display(Name = "Ngày sinh")]
    public DateTime? Dob { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [Display(Name = "Giới tính")]
    public string? Gender { get; set; }


    [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile phải là chuỗi gồm 10 chữ số.")]
    [Display(Name = "Số điện thoại")]
    public long? Mobile { get; set; }

    [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email không hợp lệ. Email phải có đuôi gmail.com.")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    public virtual ICollection<TeacherAttendance> TeacherAttendances { get; } = new List<TeacherAttendance>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; } = new List<TeacherSubject>();

    public virtual WebUser? UserNameNavigation { get; set; }
}
