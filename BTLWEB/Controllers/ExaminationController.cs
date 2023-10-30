using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEB.Models;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;
using BTLWEB.Models.TeacherAuthentication;

namespace BTLWEB.Controllers
{
    public class ExaminationController : Controller
    {
        private QuanLiSinhVienWebContext db;

        public ExaminationController(QuanLiSinhVienWebContext context)
        {
            db = context;

        }
        [TeacherAuthentication]
        public IActionResult TeacherIndex(int? page)
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "RollNo");

           
            var exams = db.Exams.AsNoTracking().OrderBy(x => x.ExamId).Include(y=>y.Class).Include(z=>z.Subject).ToList();
      

            return View(exams);
        }
       
        [HttpPost]
        [TeacherAuthentication]
        public IActionResult TeacherIndex(int classID, int subjectID, string StudentRollNumber, string TotalMarks, string OutOfMarks)
        {
            if (ModelState.IsValid)
            {
                Exam exams = new Exam();
                exams.ClassId = classID;
                exams.SubjectId = subjectID;
                exams.TotalMarks = int.Parse(TotalMarks);
                exams.OutOfMarks = int.Parse(OutOfMarks);
                
         
                exams.RollNo = StudentRollNumber;

                db.Exams.Add(exams);
                db.SaveChanges();
            }

            return RedirectToAction("TeacherIndex");
        }
        [TeacherAuthentication]
        public IActionResult TeacherEdit(int? examId)
        {
            if (examId == 0)
            {
                return NotFound();
            }
            var examt = db.Exams.Find(examId);
            if (examt == null)
            {
                return NotFound();
            }
           
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "RollNo", "RollNo");
            return View(examt);
        }
        [TeacherAuthentication]
        [HttpPost]
        public IActionResult TeacherEdit(int examId, Exam exam)
        {
            if (examId != exam.ExamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Exams.Update(exam);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(TeacherIndex));
            }
        
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");

            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            ViewBag.StudentRollNo = new SelectList(db.Students, "StudentId", "RollNo");
            return View(exam);
        }
        [TeacherAuthentication]
        public IActionResult Delete(int? examId)
        {
            if (examId == 0)
            {
                return NotFound();
            }
            var examt = db.Exams.Where(T => T.ExamId == examId).FirstOrDefault();
            if (examt == null)
            {
                return NotFound();
            }
            db.Exams.Remove(examt);
            db.SaveChanges();
         
            return RedirectToAction(nameof(TeacherIndex));
        }
        [AdminAuthentication]
        public IActionResult MarkDetails(int? page)
        {


            var exams = db.Exams.AsNoTracking().OrderBy(x => x.ExamId).Include(y => y.Class).Include(z => z.Subject).ToList();

            return View(exams);
        }
        [TeacherAuthentication]
        public IActionResult TeacherMarkdetails(int? page)
        {
            var exams = db.Exams.AsNoTracking().OrderBy(x => x.ExamId).Include(y => y.Class).Include(z => z.Subject).ToList();
          
            return View(exams);
        }
        public ActionResult GetMarkDetails(int page = 1)
        {
            int pageSize = 5; // Số lượng hiển thị trên mỗi trang
            int totalExams = db.Exams.Count(); // Tổng số 
            // Tính toán số trang và lấy danh sách cho trang hiện tại
            var exams = db.Exams.OrderBy(s => s.SubjectId).Include(y => y.Class).Include(z => z.Subject)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalExams,
                TotalPages = (int)Math.Ceiling((double)totalExams / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableMarkDetails", new MarkDetailsListViewModel { Exams = exams, Pagination = pagination });
        }
    }
}
