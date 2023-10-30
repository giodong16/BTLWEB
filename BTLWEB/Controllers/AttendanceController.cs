using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEB.Models;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;

namespace BTLWEB.Controllers
{
    public class AttendanceController : Controller
    {
        private QuanLiSinhVienWebContext db;

        public AttendanceController(QuanLiSinhVienWebContext context)
        {
            db = context;

        }
        //danh sách giáo viên
        [AdminAuthentication]
        public IActionResult Index(int? page)
        {

            var attendances = db.TeacherAttendances.AsNoTracking().OrderBy(x => x.Id).Include(x => x.Teacher).ToList();

            return View(attendances);
        }
        
        public ActionResult GetTeacherAttendances(int page = 1)
        {
            int pageSize = 5; // Số lượng hiển thị trên mỗi trang
            int totalTeacherAttendance = db.TeacherAttendances.Count(); // Tổng số 
            // Tính toán số trang và lấy danh sách cho trang hiện tại
            var teacherattendance = db.TeacherAttendances.OrderBy(s => s.Id).Include(c => c.Teacher)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalTeacherAttendance,
                TotalPages = (int)Math.Ceiling((double)totalTeacherAttendance / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableTeacherAttendance", new TeacherAttendanceListViewModel { teacherAttendances = teacherattendance, Pagination = pagination });
        }
    }
}
