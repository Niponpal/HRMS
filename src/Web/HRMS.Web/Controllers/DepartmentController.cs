using Microsoft.AspNetCore.Mvc;
namespace HRMS.Web.Controllers;
public class DepartmentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
