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
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var teachers = db.Teachers.AsNoTracking().OrderBy(x => x.TeacherId).ToList();
            PagedList<Teacher> lst = new PagedList<Teacher>(teachers, pageNumber, pageSize);

            return View(lst);
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

            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var teacherSubjects = db.TeacherSubjects.AsNoTracking().OrderBy(x => x.Id).Include(y=>y.Class).Include(z=>z.Subject).Include(t=>t.Teacher).ToList();
            PagedList<TeacherSubject> lst = new PagedList<TeacherSubject>(teacherSubjects, pageNumber, pageSize);

            return View(lst);
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
        [AdminAuthentication]
        public IActionResult Expense(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var expenses = db.Expenses.AsNoTracking().OrderBy(x => x.ExpenseId).Include(y => y.Class).Include(z => z.Subject).ToList();
            PagedList<Expense> lst = new PagedList<Expense>(expenses, pageNumber, pageSize);

            return View(lst);
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
        [AdminAuthentication]
        public IActionResult ExpenseDetails(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var expenses = db.Expenses.AsNoTracking().OrderBy(x => x.ExpenseId).Include(y => y.Class).Include(z => z.Subject).ToList();
            PagedList<Expense> lst = new PagedList<Expense>(expenses, pageNumber, pageSize);

            return View(lst);
        }
    }
}
