using DatabaseWork;
using DatabaseWork.DataClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ParserSiteWork.Models;

namespace ParserSiteWork.Controllers
{
    public class AuthorizationController : Controller
    {
        private DatabaseContext _db;

        public AuthorizationController(DatabaseContext database)
        {
            _db = database;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AutorizationModel user)
        {
            if (ModelState.IsValid)
            {
                string login = user.Login;
                try
                {
                    var user_finded = _db.Users.Include(r => r.RoleLink).Where(d => d.UserName == login).ToArray()[0];

                    if (user_finded != null && user_finded.Password == Cryptor.HashPasswordSHA512(user.Password))
                    {
                        HttpContext.Response.Cookies.Append("login_guid", Guid.NewGuid().ToString());
                        HttpContext.Response.Cookies.Append("role", user_finded.RoleLink?.Name ?? "user");

                        return Redirect("Home/Index");
                    }
                }
                catch (ArgumentNullException ex)
                { }
                catch (IndexOutOfRangeException ex)
                {
                    return Redirect("Authorization/Login");
                }
            }

            return Redirect("Authorization/Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            string? login_id = HttpContext.Request.Cookies["login_guid"] ?? "";

            if (login_id != null && login_id != "")
                return Redirect("Authorization/Login");

            return View("AuthError");
        }
    }
}
