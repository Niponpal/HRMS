using HRMS.Core.Entities.BaseEntities;

namespace HRMS.Core.Entities;

public class Shift: AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Remarks { get; set; } = string.Empty;
    public ICollection<HolidayAssignment> HolidayAssignments { get; set; } = new List<HolidayAssignment>();
    public ICollection<Holiday> Holidays { get; set; } = new List<Holiday>();
}
