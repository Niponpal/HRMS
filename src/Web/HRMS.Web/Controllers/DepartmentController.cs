using HRMS.Application.Filterl;
using HRMS.Application.Logging;
using HRMS.Application.Repositories;
using HRMS.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRMS.Web.Controllers;

[Authorize]
public class DepartmentController : Controller
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IAppLogger<DepartmentController> _logger;

    public DepartmentController(IDepartmentRepository departmentRepository, IAppLogger<DepartmentController> logger)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10)
    {
        try
        {
            var filter = new Filter
            {
                Search = search,
                IsDelete = false,
                Page = page,
                PageSize = pageSize
            };

#if DEBUG
            _logger.LogInfo("Start Stopwatch");
            var stopwatch = Stopwatch.StartNew();
#endif

            _logger.LogInfo($"Fetching departments. Search={search}, Page={page}, PageSize={pageSize}");

            //  Cantionaltokan  HttpContext.RequestAborted

            var pagination = await _departmentRepository.GetDepartmentAsync(filter, HttpContext.RequestAborted);

#if DEBUG
            _logger.LogInfo($"GetDepartmentData took {stopwatch.ElapsedMilliseconds}ms");
#endif

            _logger.LogInfo($"Fetched {pagination.Items.Count()} departments");
            return View(pagination);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while fetching departments", ex);
            return StatusCode(500, "An error occurred while fetching departments.");
        }
    }

    [HttpGet]
    [Route("department/create-or-edit/{id?}")]
    public async Task<IActionResult> CreateOrEdit(long id = 0)
    {
        try
        {
            if (id > 0)
            {
                _logger.LogInfo($"Editing Department Id={id}");
                var departmentVm = await _departmentRepository.GetDepartmentByIdAsync(id, CancellationToken.None);

                if (departmentVm == null)
                {
                    TempData["AlertMessage"] = $"Department with Id {id} not found.";
                    TempData["AlertType"] = "Error";
                    return RedirectToAction(nameof(Index));
                }

                return View(departmentVm);
            }

            return View(new DepartmentVm());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in CreateOrEdit for Id={id}", ex);
            return StatusCode(500, "An error occurred while opening the form.");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("department/create-or-edit/{id?}")]
    public async Task<IActionResult> CreateOrEdit(DepartmentVm department)
    {
        if (!ModelState.IsValid)
        {
            TempData["AlertMessage"] = "Please fix validation errors.";
            TempData["AlertType"] = "Warning";
            return View(department);
        }

        try
        {
            var result = await _departmentRepository.CreateOrUpdateDepartmentAsync(department, HttpContext.RequestAborted);

            if (result == null)
            {
                TempData["AlertMessage"] = $"Department with Id {department.Id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = department.Id > 0
                ? "Department updated successfully!"
                : "Department created successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating/updating department", ex);
            TempData["AlertMessage"] = "An error occurred while saving the department.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("department/delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var deleted = await _departmentRepository.DeleteDepartmentAsync(id, HttpContext.RequestAborted);

            if (!deleted)
            {
                TempData["AlertMessage"] = $"Department with Id {id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = "Department deleted successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting department Id={id}", ex);
            TempData["AlertMessage"] = "An error occurred while deleting the department.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }
}
