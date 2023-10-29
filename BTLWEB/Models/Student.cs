using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLWEB.Models;

public partial class Student
{
    public int StudentId { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Họ tên phải có ít nhất 4 ký tự và tối đa 50 ký tự.")]
    [Display(Name = "Họ tên")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
    [Range(typeof(DateTime), "1/1/1990", "12/31/2005", ErrorMessage = "Ngày sinh phải trong khoảng 1/1/1990 - 12/31/2005")]
    [Display(Name = "Ngày sinh")]
    public DateTime? Dob { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [Display(Name = "Giới tính")]
    public string? Gender { get; set; }


    [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile phải là chuỗi gồm 10 chữ số.")]
    [Display(Name = "Số điện thoại")]
    public long? Mobile { get; set; }

    [Required(ErrorMessage = "Mã số sinh viên là bắt buộc.")]
    [RegularExpression("^[0-9]{1,10}$", ErrorMessage = "Mã sinh viên phải là 1 chuỗi có độ dài từ 1 tới 10.")]
    [Display(Name = "Mã sinh viên")]
    public string? RollNo { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn lớp học.")]
  /*  [RegularExpression("^[0-9]{1,5}$", ErrorMessage = "Vui lòng chọn lớp học.")]*/
    [Display(Name = "Lớp học")]
    public int? ClassId { get; set; }


    public virtual Class? Class { get; set; }
}
