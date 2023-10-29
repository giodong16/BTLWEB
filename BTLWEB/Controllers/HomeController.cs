using Microsoft.AspNetCore.Mvc;
using BTLWEB.Models;
using System.Diagnostics;
using BTLWEB.Models.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BTLWEB.Models.AdminAuthentication;
using BTLWEB.Models.TeacherAuthentication;

namespace BTLWEB.Controllers
{
    public class HomeController : Controller
    {
        private QuanLiSinhVienWebContext db;
        
        public HomeController(QuanLiSinhVienWebContext context)
        {
            db = context;
        }
        private readonly ILogger<HomeController> _logger;

        /*  public HomeController(ILogger<HomeController> logger)
          {
              _logger = logger;
          }*/
        /*  [Authentication]*/
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        [AdminAuthentication]

        public IActionResult AdminDashboard()
        {
            return View();
        }
        [TeacherAuthentication]

        public IActionResult TeacherDashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}