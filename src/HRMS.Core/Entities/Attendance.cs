using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Attendance:AuditableEntity
{
    

    public long EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTime AttendanceDate { get; set; }
    public DateTime? InTime { get; set; }
    public DateTime? OutTime { get; set; }
    public string Status { get; set; } // Present, Absent, Leave, Holiday, Holiday Duty, Late
    public decimal? WorkHours { get; set; }
    public string Remarks { get; set; }

}
