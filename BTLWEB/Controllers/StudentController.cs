using Microsoft.AspNetCore.Mvc;
using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;
using System.Security.Cryptography;
using BTLWEB.Models.TeacherAuthentication;

namespace BTLWEB.Controllers
{
    public class StudentController : Controller
    {
        private QuanLiSinhVienWebContext db;
       // private List<Student> listStudents = new List<Student>();
        public StudentController(QuanLiSinhVienWebContext context)
        {
            db = context;
        }
        [AdminAuthentication]
        public IActionResult Index(int? page)
        {
            
            var students = db.Students.AsNoTracking().OrderBy(x => x.RollNo).Include(m => m.Class).ToList();
            

            return View(students);
        }

        [AdminAuthentication]
        [HttpGet]
        public IActionResult Create()
        {   
            
            ViewBag.ClassId =  new SelectList(db.Classes, "ClassId", "ClassName");
            return View();
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId =  new SelectList(db.Classes, "ClassId", "ClassName");
            
            return View(student);
        }
        [AdminAuthentication]
        public IActionResult Edit(int? mid)
        {

            if (mid == null || db.Students == null)
            {
                return NotFound();
            }
            var student = db.Students.Find(mid);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            return View(student);
        }

        [AdminAuthentication]
        [HttpPost]
        public IActionResult Edit(int mid, Student student)
        {
            if (mid != student.StudentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(student);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }


                return RedirectToAction(nameof(Index));
            }

           
            var classes = new SelectList(db.Classes, "ClassId", "ClassName", student.ClassId);
            ViewBag.ClassId = classes;
            return View(student);
        }
        [AdminAuthentication]
        [HttpGet]
        public IActionResult Delete(int? mid)
        {

            if (mid == null || db.Students == null)
            {
                return NotFound();
            }

            var student = db.Students.Find(mid);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [AdminAuthentication]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int mid)
        {
            if (db.Students == null)
            {
                return NotFound();
            }

            var student = db.Students.Find(mid);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [TeacherAuthentication]
        public IActionResult Attendance(int? page)
        {

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "Name");

            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var attendances = db.StudentAttendances.AsNoTracking().Include(x => x.Class).Include(y => y.Subject).ToList();
            PagedList<StudentAttendance> lst = new PagedList<StudentAttendance>(attendances, pageNumber, pageSize);
            return View(lst);
        }
        [TeacherAuthentication]
        [HttpPost]
        public IActionResult Attendance(int classID, int subjectID, string StudentRollNo, DateTime date)
        {
            if (ModelState.IsValid)
            {
                StudentAttendance studentAttendance = new StudentAttendance();
                studentAttendance.ClassId = classID;
                studentAttendance.SubjectId = subjectID;
                studentAttendance.RollNo = StudentRollNo;
                studentAttendance.Date = date;
                studentAttendance.Status = true;
                db.StudentAttendances.Add(studentAttendance);
                db.SaveChanges();
            }

            return RedirectToAction("Attendance");
        }
        [TeacherAuthentication]
        public IActionResult deleteAttendance(int? StudentAttendanceId)
        {
            if (StudentAttendanceId == 0 || StudentAttendanceId == null)
            {
                return NotFound();
            }
            var studentAttendance = db.StudentAttendances.Find(StudentAttendanceId);
            if (studentAttendance == null)
            {
                return NotFound();
            }
            db.StudentAttendances.Remove(studentAttendance);
            db.SaveChanges();
      
            return RedirectToAction(nameof(Attendance));
        }
        [TeacherAuthentication]
        public IActionResult TeacherAttendance(int? page)
        {

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "Name");

          
            var attendances = db.StudentAttendances.AsNoTracking().Include(x => x.Class).Include(y => y.Subject).ToList();

            return View(attendances);
        }
        [TeacherAuthentication]
        [HttpPost]
        public IActionResult TeacherAttendance(int classID, int subjectID, string StudentRollNo, DateTime date)
        {
            if (ModelState.IsValid)
            {
                StudentAttendance studentAttendance = new StudentAttendance();
                studentAttendance.ClassId = classID;
                studentAttendance.SubjectId = subjectID;
                studentAttendance.RollNo = StudentRollNo;
                studentAttendance.Date = date;
                studentAttendance.Status = true;
                db.StudentAttendances.Add(studentAttendance);
                db.SaveChanges();
            }

            return RedirectToAction("TeacherAttendance");
        }
        [TeacherAuthentication]
        public IActionResult TeacherEditAttendance(int? StudentAttendanceId)
        {
            if (StudentAttendanceId == null || db.StudentAttendances == null)
            {
                return NotFound();
            }
            var studentAttendance = db.StudentAttendances.Find(StudentAttendanceId);
            if (studentAttendance == null)
            {
                return NotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "Name");
            return View(studentAttendance);

        }
        [TeacherAuthentication]
        [HttpPost]
        public IActionResult TeacherEditAttendance(int StudentAttendanceId,StudentAttendance studentAttendance)
        {
           /* if (StudentAttendanceId != studentAttendance.Id)
            {
                return NotFound();
            }
*/
            if (ModelState.IsValid)
            {
                try
                {
                    studentAttendance.Status = true;
                    db.StudentAttendances.Update(studentAttendance);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(TeacherAttendance));
            }
            
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "Name");
            return View(studentAttendance);
        }
        [TeacherAuthentication]
        public IActionResult deleteTeacherAttendance(int? StudentAttendanceId)
        {
            if (StudentAttendanceId == 0 || StudentAttendanceId == null)
            {
                return NotFound();
            }
            var studentAttendance = db.StudentAttendances.Find(StudentAttendanceId);
            if (studentAttendance == null)
            {
                return NotFound();
            }
            db.StudentAttendances.Remove(studentAttendance);
            db.SaveChanges();
            return RedirectToAction(nameof(TeacherAttendance));
        }
        public ActionResult GetStudents(int page = 1)
        {
            int pageSize = 5; // Số lượng sinh viên hiển thị trên mỗi trang
            int totalStudents = db.Students.Count(); // Tổng số sinh viên

            // Tính toán số trang và lấy danh sách sinh viên cho trang hiện tại
            var students = db.Students.OrderBy(s => s.StudentId).Include(c=>c.Class)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalStudents,
                TotalPages = (int)Math.Ceiling((double)totalStudents / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_StudentList", new StudentListViewModel { Students = students, Pagination = pagination });
        }
        public ActionResult GetStudentAttendances(int page = 1)
        {
            int pageSize = 3; // Số lượnghiển thị trên mỗi trang
            int totalStudents = db.StudentAttendances.Count(); // Tổng số

            // Tính toán số trang và lấy danh sách cho trang hiện tại
            var studentAttendances = db.StudentAttendances.OrderBy(s => s.Id).Include(x => x.Class).Include(y => y.Subject)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalStudents,
                TotalPages = (int)Math.Ceiling((double)totalStudents / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableStudentAttendance", new StudentAttendanceListViewModel { studentAttendances = studentAttendances, Pagination = pagination });
        }
    }
}
