using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEB.Models;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;

namespace BTLWEB.Controllers
{
    public class TeacherController : Controller
    {
        private QuanLiSinhVienWebContext db;
      
        public TeacherController(QuanLiSinhVienWebContext context)
        {
            db = context;
        }
        [AdminAuthentication]
        public IActionResult Index(int? page)
        {
          
            var teachers = db.Teachers.AsNoTracking().OrderBy(x => x.TeacherId).ToList();
         

            return View(teachers);
        }
        public ActionResult GetTeachers(int? idTeacher, int page = 1)
        {

            int pageSize = 3;
            int totalTeachers = db.Teachers.Count(); 

          
            var teachers = db.Teachers.OrderBy(s => s.TeacherId)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalTeachers,
                TotalPages = (int)Math.Ceiling((double)totalTeachers / pageSize)
            };

       
            return PartialView("_TeacherList", new TeacherListViewModel { Teachers = teachers, Pagination = pagination });
        }

        [AdminAuthentication]
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }
        [AdminAuthentication]
        public IActionResult Edit(int? mid)
        {

            if (mid == null || db.Teachers == null)
            {
                return NotFound();
            }
            var teacher = db.Teachers.Find(mid);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [AdminAuthentication]
        [HttpPost]
        public IActionResult Edit(int mid, Teacher teacher)
        {
            if (mid != teacher.TeacherId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(teacher);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }


                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
        [AdminAuthentication]
        [HttpGet]
        public IActionResult Delete(int? mid)
        {

            if (mid == null || db.Teachers == null)
            {
                return NotFound();
            }

            var teacher = db.Teachers.Find(mid);

            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        [AdminAuthentication]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int mid)
        {
            if (db.Teachers == null)
            {
                return NotFound();
            }

            var teacher = db.Teachers.Find(mid);
            if (teacher == null)
            {
                return NotFound();
            }
            var teacherSubjects=db.TeacherSubjects.Where(t => t.TeacherId == mid).FirstOrDefault();
            if (teacherSubjects != null)
            {
                
                return Content("Dữ liệu về giảng viên " + teacher.Name+" không thể xóa do có môn học do giảng viên giảng dạy");
            }
            var teacherAttendance = db.TeacherAttendances.Where(t => t.TeacherId == mid).FirstOrDefault();
            if (teacherAttendance != null)
            {

                return Content("Dữ liệu về giảng viên " + teacher.Name + " không thể xóa giảng viên đang tham gia giảng dạy");
            }
            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [AdminAuthentication]
        public IActionResult TeacherSubject(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");

          
            var teacherSubjects = db.TeacherSubjects.AsNoTracking().OrderBy(x => x.Id).Include(y=>y.Class).Include(z=>z.Subject).Include(t=>t.Teacher).ToList();
            

            return View(teacherSubjects);
        }
        public ActionResult GetTeacherSubject(int page = 1)
        {
            int pageSize = 2; 
            int totalTeacherSubject = db.TeacherSubjects.Count(); 
            var TeacherSubjects = db.TeacherSubjects.AsNoTracking().Include(x => x.Class).Include(m => m.Subject).Include(n => n.Teacher)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalTeacherSubject,
                TotalPages = (int)Math.Ceiling((double)totalTeacherSubject / pageSize)
            };

          
            return PartialView("_TeacherSubjectList", new TeacherSubjectList { TeacherSubject = TeacherSubjects, Pagination = pagination });
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult TeacherSubject(int classID, int subjectID,int teacherID)
        {
            if (ModelState.IsValid)
            {
                TeacherSubject teacherSubject = new TeacherSubject();
                teacherSubject.ClassId = classID;
                teacherSubject.SubjectId = subjectID;
                teacherSubject.TeacherId = teacherID;
                db.TeacherSubjects.Add(teacherSubject);
                db.SaveChanges();
            }

            return RedirectToAction("TeacherSubject");
        }
        public IActionResult DeleteTeacherSubject(int? mid)
        {
            if(mid== null|| mid == 0)
            {
                return NotFound();
            }
            var teachersubjects = db.TeacherSubjects.Where(t=>t.Id==mid).FirstOrDefault();
            if(teachersubjects == null)
            {
                return NotFound();
            }
            db.TeacherSubjects.Remove(teachersubjects); db.SaveChanges();
            return RedirectToAction("TeacherSubject");
        }
        [AdminAuthentication]
        public IActionResult Expense(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

           
            var expenses = db.Expenses.AsNoTracking().OrderBy(x => x.ExpenseId).Include(y => y.Class).Include(z => z.Subject).ToList();
        

            return View(expenses);
        }
        public ActionResult GetExpense(int page = 1)
        {
            int pageSize = 2; 
            int totalExpense = db.Expenses.Count(); 

           
            var Expenses = db.Expenses.AsNoTracking().Include(x => x.Class).Include(s => s.Subject)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalExpense,
                TotalPages = (int)Math.Ceiling((double)totalExpense / pageSize)
            };

          
            return PartialView("_ExpenseList", new ExpenseList { Expenses = Expenses, Pagination = pagination });
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult Expense(int classID, int subjectID, string ChargeAmount)
        {
            if (ModelState.IsValid)
            {
                Expense expense = new Expense();
                expense.ClassId = classID;
                expense.SubjectId = subjectID;
                expense.ChargeAmount=int.Parse(ChargeAmount);
                db.Expenses.Add(expense);
                db.SaveChanges();
            }

            return RedirectToAction("Expense");
        }

        
             public IActionResult deleteExpense(int? mid)
        {
            if (mid == null || mid == 0)
            {
                return NotFound();
            }
            var expense = db.Expenses.Where(x=>x.ExpenseId==mid).FirstOrDefault();
            if (expense == null)
            {
                return NotFound();
            }
            db.Expenses.Remove(expense); db.SaveChanges();
            return RedirectToAction("Expense");
        }
        [AdminAuthentication]
        public IActionResult ExpenseDetails(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");


            var expenses = db.Expenses.AsNoTracking().OrderBy(x => x.ExpenseId).Include(y => y.Class).Include(z => z.Subject).ToList();


            return View(expenses);
        }
        public ActionResult GetExpenseDetail(int page = 1)
        {
            int pageSize = 2; // Số lượng sinh viên hiển thị trên mỗi trang
            int totalExpense = db.Expenses.Count(); // Tổng số sinh viên

            // Tính toán số trang và lấy danh sách sinh viên cho trang hiện tại
            var Expenses = db.Expenses.AsNoTracking().Include(x => x.Class).Include(s => s.Subject)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalExpense,
                TotalPages = (int)Math.Ceiling((double)totalExpense / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_ExpenseDetailsList", new ExpenseList { Expenses = Expenses, Pagination = pagination });
        }
    }
}
