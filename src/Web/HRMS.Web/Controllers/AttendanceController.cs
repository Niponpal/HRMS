using HRMS.Application.Filterl;
using HRMS.Application.Logging;
using HRMS.Application.Repositories;
using HRMS.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRMS.Web.Controllers;

public class AttendanceController : Controller
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IAppLogger<AttendanceController> _logger;

    public AttendanceController(IAttendanceRepository attendanceRepository, IAppLogger<AttendanceController> logger)
    {
        _attendanceRepository = attendanceRepository;
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

            _logger.LogInfo($"Fetching  Attendances. Search={search}, Page={page}, PageSize={pageSize}");

            //  cancellationToken  HttpContext.RequestAborted


            var pagination = await _attendanceRepository.GetAttendanceVmAsync(filter, HttpContext.RequestAborted);

#if DEBUG
            _logger.LogInfo($"GetAttendancesData took {stopwatch.ElapsedMilliseconds}ms");
#endif

            _logger.LogInfo($"Fetched {pagination.Items.Count()} Attendances");

            return View(pagination);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while fetching attendances", ex);
            return StatusCode(500, "An error occurred while fetching attendances.");
        }
    }

    [HttpGet]
    [Route("attendances/create-or-edit/{id?}")]
    public async Task<IActionResult> CreateOrEdit(long id = 0)
    {
        try
        {
            if (id > 0)
            {
                _logger.LogInfo($"Editing attendances Id={id}");
                var attendanceVm = await _attendanceRepository.GetAttendanceVmByIdAsync(id, CancellationToken.None);
                if (attendanceVm == null)
                {
                    TempData["AlertMessage"] = $"attendances with Id {id} not found.";
                    TempData["AlertType"] = "Error";
                    return RedirectToAction(nameof(Index));
                }

                return View(attendanceVm);
            }
            return View(new AttendanceVm());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in CreateOrEdit for Id={id}", ex);
            return StatusCode(500, "An error occurred while opening the form.");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("attendances/create-or-edit/{id?}")]

    public async Task<IActionResult> CreateOrEdit(AttendanceVm  attendanceVm)
    {
        if (!ModelState.IsValid)
        {
            TempData["AlertMessage"] = "Please fix validation errors.";
            TempData["AlertType"] = "Warning";
            return View(attendanceVm);
        }

        try
        {
            var result = await _attendanceRepository.CreateOrUpdateAttendanceVmAsync(attendanceVm, HttpContext.RequestAborted);

            if (result == null)
            {
                TempData["AlertMessage"] = $"attendance with Id {attendanceVm.Id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = attendanceVm.Id > 0
                ? "attendance updated successfully!"
                : "attendance created successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while creating/updating attendance", ex);
            TempData["AlertMessage"] = "An error occurred while saving the department.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }
    [HttpPost]
    [Route("attendance/delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var deleted = await _attendanceRepository.DeleteAttendanceVmAsync(id, HttpContext.RequestAborted);

            if (!deleted)
            {
                TempData["AlertMessage"] = $"Department with Id {id} not found.";
                TempData["AlertType"] = "Error";
                return NotFound();
            }

            TempData["AlertMessage"] = "attendance deleted successfully!";
            TempData["AlertType"] = "Success";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while deleting attendance Id={id}", ex);
            TempData["AlertMessage"] = "An error occurred while deleting the attendance.";
            TempData["AlertType"] = "Error";
            return StatusCode(500);
        }
    }

}

