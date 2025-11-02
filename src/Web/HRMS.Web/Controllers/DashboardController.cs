using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
