using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using BTLWEB.Models.AdminAuthentication;
using BTLWEB.Models.TeacherAuthentication;

namespace BTLWEB.Controllers
{
    public class WebUserController : Controller
    {
        private QuanLiSinhVienWebContext db;

        public WebUserController(QuanLiSinhVienWebContext context)
        {
            db = context;

        }
        //của admin
        [AdminAuthentication]
        public IActionResult Upload()
        {
            string username = HttpContext.Session.GetString("UserName"); 
            string type = HttpContext.Session.GetString("UserType");
            var users = db.WebUsers.FirstOrDefault(x => x.UserName.Equals(username));
            if (users.Avatar == null)
            {
                users.Avatar = "background.png";
            }
            return View(users);
        }
        [HttpPost]
        [AdminAuthentication]
        public IActionResult Upload(IFormFile avatar)
        {
            string username = HttpContext.Session.GetString("UserName");
            string type = HttpContext.Session.GetString("UserType");
            var webUser = db.WebUsers.FirstOrDefault(x => x.UserName.Equals(username));
            if (ModelState.IsValid)
            {
                // Lưu trữ tệp hình ảnh vào thư mục của dự án
                string fileName = Path.GetFileName(avatar.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatar.CopyTo(fileStream);
                }
                webUser.Avatar = fileName;
                webUser.Loai = type;
                db.Update(webUser);
                db.SaveChanges();

                return View(webUser); ; // Chuyển hướng đến trang danh sách người dùng
            }

            return View(webUser);
        }
        [TeacherAuthentication]
        public IActionResult TeacherUpload()
        {
            string username = HttpContext.Session.GetString("UserName");
            string type = HttpContext.Session.GetString("UserType");
            var users = db.WebUsers.FirstOrDefault(x => x.UserName.Equals(username));
            if (users.Avatar == null)
            {
                users.Avatar = "background.png";
            }
            return View(users);
        }
        [HttpPost]
        [TeacherAuthentication]
        public IActionResult TeacherUpload(IFormFile avatar)
        {
            string username = HttpContext.Session.GetString("UserName");
            string type = HttpContext.Session.GetString("UserType");
            var webUser = db.WebUsers.FirstOrDefault(x => x.UserName.Equals(username));
            if (ModelState.IsValid)
            {
                // Lưu trữ tệp hình ảnh vào thư mục của dự án
                string fileName = Path.GetFileName(avatar.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatar.CopyTo(fileStream);
                }
                webUser.Avatar = fileName;
                webUser.Loai = type;
                db.Update(webUser);
                db.SaveChanges();

                return View(webUser); ; // Chuyển hướng đến trang danh sách người dùng
            }

            return View(webUser);
        }
    }
}
