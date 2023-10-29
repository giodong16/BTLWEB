using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEB.Models;
using System.Security.Cryptography;
using X.PagedList;
using BTLWEB.Models.AdminAuthentication;

namespace BTLWEB.Controllers
{
    public class ClassController : Controller
    {
        private QuanLiSinhVienWebContext db;
        
        public ClassController(QuanLiSinhVienWebContext context)
        {
            db = context;
           
        }
        [AdminAuthentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var classes = db.Classes.AsNoTracking().OrderBy(x => x.ClassId).ToList();
            PagedList<Class> lst = new PagedList<Class>(classes, pageNumber, pageSize);

            return View(lst);
        }
        [AdminAuthentication]
        [HttpPost]
        public IActionResult Index(string className)
        {
            Class newclass = new Class();
            newclass.ClassName= className;
            if (ModelState.IsValid)
            {
                db.Classes.Add(newclass);
                db.SaveChanges();
          
            }

            return RedirectToAction("Index");
        }
        [AdminAuthentication]
        [HttpPost]
        public bool EditClass(int classId, string className)
        {
            var classes1 =db.Classes.OrderBy(x => x.ClassId).ToList();
            // Find the class with the given id in the list
            var lop = classes1.FirstOrDefault(c => c.ClassId == classId);
            // If the class exists and the new name is not empty, update the class name and return true
            if (lop != null && !string.IsNullOrEmpty(className))
            {
                lop.ClassName = className;
                return true;
            }
            // Otherwise, return false
            else
            {
                return false;
            }
        }

        //fees
        [AdminAuthentication]
        public IActionResult ClassFees(int? page)
        {

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var feess = db.Fees.AsNoTracking().OrderBy(x => x.FeesId).Include(y =>y.Class).ToList();
            PagedList<Fee> lst = new PagedList<Fee>(feess, pageNumber, pageSize);
            
            return View(lst);
        }
        [HttpPost]
        [AdminAuthentication]
        public IActionResult ClassFees(int classID, string FeesAmount)
        {
            Fee newfees = new Fee();
            int id = classID;
            int amount= int.Parse(FeesAmount);
            if (id== 0) { id = 2; }
            newfees.ClassId = id;
            newfees.FeesAmount = amount;
            if (ModelState.IsValid)
            {
                db.Fees.Add(newfees);
                db.SaveChanges();
            }

            return RedirectToAction("classFees");
        }
    }
}

