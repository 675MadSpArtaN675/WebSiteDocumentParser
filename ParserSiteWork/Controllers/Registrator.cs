using Microsoft.AspNetCore.Mvc;
using DatabaseWork;
using DatabaseWork.DataClasses;

class Registration : Controller
{
    private DatabaseContext db;

    public Registration(DatabaseContext _db)
    {
        db = _db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Request.Cookies["login_guid"] != null && HttpContext.Request.Cookies["role"] != null)
        {
            return View();
        }

        return Redirect("/DataWorker/Index");
    }

    [HttpPost]
    public IActionResult Index(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            User new_user = new User();
            new_user.UserName = model.Login;

            if (model.Password != null && model.Password == model.PasswordConfrim)
                new_user.Password = Cryptor.HashPasswordSHA512(model.Password);

            db.Users.Add(new_user);
            db.SaveChanges();
        }

        return Redirect("/Registration/Index");
    }
}