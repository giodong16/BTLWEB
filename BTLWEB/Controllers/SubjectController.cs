using Microsoft.AspNetCore.Mvc;
using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;

namespace BTLWEB.Controllers
{
    public class SubjectController : Controller
    {
        private QuanLiSinhVienWebContext db;


        public SubjectController(QuanLiSinhVienWebContext context)
        {
            db = context;

        }
        [AdminAuthentication]
        public IActionResult Index(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            
        
            var subjects = db.Subjects.AsNoTracking().OrderBy(x => x.SubjectId).Include(y=>y.Class).ToList();


            return View(subjects);
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult Index(int classID, string SubjectName)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject();
                subject.ClassId = classID;
                subject.SubjectName = SubjectName;
                db.Subjects.Add(subject);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [AdminAuthentication]
        public IActionResult deleteSubject(int? mid)
        {

            if(mid==0 || mid == null)
            {
                return NotFound();
            }
            var subject = db.Subjects.Find(mid);
            if (subject == null)
            {
                return NotFound("Error");
            }
            var exam =db.Exams.Where(x=>x.SubjectId==mid).FirstOrDefault();
            var expense=db.Expenses.Where(x=>x.SubjectId==mid).FirstOrDefault();
            var teachersubject=db.TeacherSubjects.Where(x => x.SubjectId == mid).FirstOrDefault();
            var studentattendance = db.StudentAttendances.Where(x => x.SubjectId == mid).FirstOrDefault();
            if(exam!= null) {
                return Content("Dữ liệu về " + subject.SubjectName + " không thể xóa do có bài kiểm tra về môn này");
            }
            if (expense != null)
            {
                return Content("Dữ liệu về " + subject.SubjectName + " không thể xóa do còn thông tin ở bảng chi phí - Chi phí giảng dạy");
            }
            if (teachersubject != null)
            {
                return Content("Dữ liệu về " + subject.SubjectName + " không thể xóa do có giáo viên đăng kí dạy môn này");
            }
            if (studentattendance != null)
            {
                return Content("Dữ liệu về " + subject.SubjectName + " không thể xóa do có sinh viên đăng kí học môn này");
            }

            db.Subjects.Remove(subject);db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult GetSubjects(int page = 1)
        {
            int pageSize = 5; // Số lượng hiển thị trên mỗi trang
            int totalSubjects = db.Subjects.Count(); // Tổng số 
            // Tính toán số trang và lấy danh sách cho trang hiện tại
            var subjects = db.Subjects.OrderBy(s => s.SubjectId).Include(c => c.Class)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalSubjects,
                TotalPages = (int)Math.Ceiling((double)totalSubjects / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableSubject", new SubjectListViewModel { Subjects = subjects, Pagination = pagination });
        }
    }
}
