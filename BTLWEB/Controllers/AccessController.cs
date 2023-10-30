using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEB.Models;
using System.Security.Cryptography;
using X.PagedList;

namespace BTLWEB.Controllers
{

    public class AccessController : Controller
    {
        private QuanLiSinhVienWebContext db;

        public AccessController(QuanLiSinhVienWebContext context)
        {
            db = context;

        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                string name = HttpContext.Session.GetString("UserName");
                string loai = HttpContext.Session.GetString("UserType");
                if (loai == "admin")
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (loai == "teacher")
                {
                    return RedirectToAction("TeacherDashboard", "Home");
                }
                else
                {
                    /*context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Home" },
                            {"Action", "Error" }
                        });*/
                }
                return View();

            }
            
        }
        [HttpPost]
     
        public IActionResult Login(WebUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.WebUsers.FirstOrDefault(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
                    HttpContext.Session.SetString("UserType", u.Loai.ToString());

                    if (u.Loai == "admin")
                    {
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else if (u.Loai == "teacher")
                    {
                        return RedirectToAction("TeacherDashboard", "Home");
                    }
                    else
                    {
                        return RedirectToAction("home", "Home");
                    }
                }
            }

            // Đăng nhập không đúng, trả về trang login
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

       
    }
}
