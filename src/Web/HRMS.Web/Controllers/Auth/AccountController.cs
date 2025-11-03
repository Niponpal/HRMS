using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers.Auth;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
