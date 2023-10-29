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
            
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var subjects = db.Subjects.AsNoTracking().OrderBy(x => x.SubjectId).Include(y=>y.Class).ToList();
            PagedList<Subject> lst = new PagedList<Subject>(subjects, pageNumber, pageSize);

            return View(lst);
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
    }
}
