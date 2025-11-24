using HRMS.Application.Filterl;
using HRMS.Application.Logging;
using HRMS.Application.Repositories;
using HRMS.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRMS.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAppLogger<EmployeeController> _logger;

    public EmployeeController(IEmployeeRepository employeeRepository, IAppLogger<EmployeeController> logger)
    {
        _employeeRepository = employeeRepository;
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

            _logger.LogInfo($"Fetching employees. Search={search}, Page={page}, PageSize={pageSize}");

            //  Cantionaltokan  HttpContext.RequestAborted

            var pagination = await _employeeRepository.GetEmployeeAsync(filter, HttpContext.RequestAborted);

#if DEBUG
            _logger.LogInfo($"GetemployeeData took {stopwatch.ElapsedMilliseconds}ms");
#endif

            _logger.LogInfo($"Fetched {pagination.Items.Count()} employees");
            return View(pagination);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while fetching employeess", ex);
            return StatusCode(500, "An error occurred while fetching employeess.");
        }
    }

    [HttpGet]
    [Route("employee/create-or-edit/{id?}")]
    public async Task<IActionResult> CreateOrEdit(long id = 0)
    {
        try
        {
            if (id > 0)
            {
                _logger.LogInfo($"Editing employee Id={id}");
                var employeeVm = await _employeeRepository.GetEmployeeByIdAsync(id, CancellationToken.None);

                if (employeeVm == null)
                {
                    TempData["AlertMessage"] = $"employee with Id {id} not found.";
                    TempData["AlertType"] = "Error";
                    return RedirectToAction(nameof(Index));
                }

                return View(employeeVm);
            }

            return View(new EmployeeVm());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in CreateOrEdit for Id={id}", ex);
            return StatusCode(500, "An error occurred while opening the form.");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("employee/create-or-edit/{id?}")]
    public async Task<IActionResult> CreateOrEdit(EmployeeVm  employee)
    {
        if (!ModelState.IsValid)
        {
            TempData["AlertMessage"] = "Please fix validation errors.";
            TempData["AlertType"] = "Warning";
            return View(employee);
        }

        try
        {
            var result = await _employeeRepository.CreateOrUpdateEmployeeAsync(employee, HttpContext.RequestAborted);

            if (result == null)
            {
                TempData["AlertMessage"] = $"employee with Id {employee.Id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = employee.Id > 0
                ? "employee updated successfully!"
                : "employee created successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating/updating employee", ex);
            TempData["AlertMessage"] = "An error occurred while saving the employee.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("employee/delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var deleted = await _employeeRepository.DeleteEmployeeAsync(id, HttpContext.RequestAborted);

            if (!deleted)
            {
                TempData["AlertMessage"] = $"employee with Id {id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = "employee deleted successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting employee Id={id}", ex);
            TempData["AlertMessage"] = "An error occurred while deleting the employee.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }

   

}
