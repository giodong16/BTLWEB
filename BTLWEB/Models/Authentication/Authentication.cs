using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTLWEB.Models.Authentication
{
    public class Authentication: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller","Access" },
                        {"Action","Login" }
                    });
            }
            else 
            {
                string name = context.HttpContext.Session.GetString("UserName");
                string loai = context.HttpContext.Session.GetString("UserType");
                if (loai == "admin")
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Home" },
                            {"Action", "AdminDashboard" }
                        });
                }
                else if(loai == "teacher")
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Home" },
                            {"Action", "TeacherDashboard" }
                        });
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
            }

        }
  

    }
}
