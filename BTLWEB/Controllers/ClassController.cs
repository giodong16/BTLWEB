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
            
            var classes = db.Classes.AsNoTracking().OrderBy(x => x.ClassId).ToList();
            

            return View(classes);
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
        //fees
        [AdminAuthentication]
        public IActionResult ClassFees(int? page)
        {
    

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
          
            var feess = db.Fees.AsNoTracking().OrderBy(x => x.FeesId).Include(y =>y.Class).ToList();
           
            
            return View(feess);
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
        [AdminAuthentication]
        public IActionResult deleteFees(int? mid)
        {

            if (mid == null || mid == 0)
            {
                return NotFound();
            }

            var fee = db.Fees.Find(mid);
            if (fee == null)
            {
                return NotFound();
            }

            db.Fees.Remove(fee); db.SaveChanges();
            return RedirectToAction("classFees");
        }
        public ActionResult GetClass(int page = 1)
        {
            int pageSize = 5; // Số lượng class hiển thị trên mỗi trang
            int totalClasses = db.Classes.Count(); // Tổng số sinh viên

            // Tính toán số trang và lấy danh sách class cho trang hiện tại
            var classes = db.Classes.OrderBy(s => s.ClassId)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalClasses,
                TotalPages = (int)Math.Ceiling((double)totalClasses / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableClass", new ClassListViewModel { Classes = classes, Pagination = pagination });
        }
        public ActionResult GetFees(int page = 1)
        {
            int pageSize = 2; // Số lượng  hiển thị trên mỗi trang
            int totalFees = db.Fees.Count(); // Tổng số

            // Tính toán số trang và lấy danh sách  cho trang hiện tại
            var fees = db.Fees.OrderBy(s => s.FeesId).Include(x=>x.Class)
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToList();

            // Tạo đối tượng phân trang
            var pagination = new PaginationViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalFees,
                TotalPages = (int)Math.Ceiling((double)totalFees / pageSize)
            };

            // Trả về Partial View chứa danh sách sinh viên và phân trang
            return PartialView("_TableFee", new FeeListViewModel { Fees = fees, Pagination = pagination });
        }
    }
}

