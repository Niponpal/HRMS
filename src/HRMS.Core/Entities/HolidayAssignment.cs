using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class HolidayAssignment:AuditableEntity
{
    public long HolidayId { get; set; }
    public Holiday Holiday { get; set; }
    public long DepartmentId { get; set; }
    public Department Department { get; set; }
    public long ShiftId { get; set; }
    public Shift Shift { get; set; }

}
