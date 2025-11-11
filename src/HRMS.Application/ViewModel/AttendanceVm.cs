using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Application.ViewModel;

public class AttendanceVm:BaseEntity
{
    public DateTime AttendanceDate { get; set; }
    public DateTime? InTime { get; set; }
    public DateTime? OutTime { get; set; }
    public string Status { get; set; } // Present, Absent, Leave, Holiday, Holiday Duty, Late
    public decimal? WorkHours { get; set; }
    public string Remarks { get; set; }

}
