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
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var attendances = db.TeacherAttendances.AsNoTracking().OrderBy(x => x.Id).Include(x => x.Teacher).ToList();
            PagedList<TeacherAttendance> lst = new PagedList<TeacherAttendance>(attendances, pageNumber, pageSize);
            return View(lst);
        }
      
    }
}
